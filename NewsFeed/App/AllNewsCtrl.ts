interface IAllNewsCtrlScope {
    items;
    viewNews;
    singleNews;
    title;
}


class AllNewsCtrl {
    static $inject: string[] = ["$scope", "$http", "$filter", "constants"];

    constructor(private $scope: IAllNewsCtrlScope, private $http, private $filter, private constants) {
        this.$scope.title = "All News";
        this.$scope.viewNews = (id) => this.viewNews(id);

        $http.get(constants.urlallnews)
            .then(
            (response) => {
                this.$scope.items = response.data.Data;
                this.$scope.singleNews = this.$scope.items[0];
            },
                (error) => {
                    console.error(error);
                }
        );

    }
    

    viewNews(id) {
        this.$scope.singleNews = this.$scope.items[id];
    }

}
angular.module("app").controller("AllNewsCtrl", AllNewsCtrl);;