var AllNewsCtrl = /** @class */ (function () {
    function AllNewsCtrl($scope, $http, $filter, constants) {
        var _this = this;
        this.$scope = $scope;
        this.$http = $http;
        this.$filter = $filter;
        this.constants = constants;
        this.$scope.title = "All News";
        this.$scope.viewNews = function (id) { return _this.viewNews(id); };
        $http.get(constants.urlallnews)
            .then(function (response) {
            _this.$scope.items = response.data.Data;
            _this.$scope.singleNews = _this.$scope.items[0];
        }, function (error) {
            console.error(error);
        });
    }
    AllNewsCtrl.prototype.viewNews = function (id) {
        this.$scope.singleNews = this.$scope.items[id];
    };
    AllNewsCtrl.$inject = ["$scope", "$http", "$filter", "constants"];
    return AllNewsCtrl;
}());
angular.module("app").controller("AllNewsCtrl", AllNewsCtrl);
;
//# sourceMappingURL=AllNewsCtrl.js.map