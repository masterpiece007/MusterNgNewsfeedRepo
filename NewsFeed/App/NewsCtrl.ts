interface INewsCtrlScope {
    items;
    viewNews;
    singleNews;
    title;
    search;
    word;
    defaultNews;
    cancel;
}


class NewsCtrl {
    static $inject: string[] = ["$scope", "$http", "$filter", "constants"];

    constructor(private $scope: INewsCtrlScope, private $http, private $filter, private constants) {
        this.$scope.viewNews = (id) => this.viewNews(id);
        this.$scope.title = "My News";

        $http.get(constants.urlusernews)
            .then(
            (response) => {
                this.$scope.items = response.data.Data;
                this.$scope.defaultNews = response.data.Data;
                this.$scope.viewNews(this.$scope.items[0]);

                console.log(this.$scope.items[0]);
                this.$scope.singleNews = this.$scope.items[0];
            },
                (error) => {
                    console.error(error);
                }
        );

        this.$scope.search = () => {
            this.$scope.items = null;
            this.$scope.items = [];
            angular.forEach(this.$scope.defaultNews,
                (item, index) => {
                    if (item.Title.search(this.$scope.word) !== -1) {
                        this.$scope.items.push(item);
                    }
                });
        }

        this.$scope.cancel = () => {
            this.$scope.items = null;
            this.$scope.items = this.$scope.defaultNews;
        }
    }
    

    viewNews(id) {
        this.$scope.singleNews = this.$scope.items[id];
    }

}
angular.module("app").controller("NewsCtrl", NewsCtrl);;