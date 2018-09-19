var NewsCtrl = /** @class */ (function () {
    function NewsCtrl($scope, $http, $filter, constants) {
        var _this = this;
        this.$scope = $scope;
        this.$http = $http;
        this.$filter = $filter;
        this.constants = constants;
        this.$scope.viewNews = function (id) { return _this.viewNews(id); };
        this.$scope.title = "My News";
        $http.get(constants.urlusernews)
            .then(function (response) {
            _this.$scope.items = response.data.Data;
            _this.$scope.defaultNews = response.data.Data;
            _this.$scope.viewNews(_this.$scope.items[0]);
            console.log(_this.$scope.items[0]);
            _this.$scope.singleNews = _this.$scope.items[0];
        }, function (error) {
            console.error(error);
        });
        this.$scope.search = function () {
            _this.$scope.items = null;
            _this.$scope.items = [];
            angular.forEach(_this.$scope.defaultNews, function (item, index) {
                if (item.Title.search(_this.$scope.word) !== -1) {
                    _this.$scope.items.push(item);
                }
            });
        };
        this.$scope.cancel = function () {
            _this.$scope.items = null;
            _this.$scope.items = _this.$scope.defaultNews;
        };
    }
    NewsCtrl.prototype.viewNews = function (id) {
        this.$scope.singleNews = this.$scope.items[id];
    };
    NewsCtrl.$inject = ["$scope", "$http", "$filter", "constants"];
    return NewsCtrl;
}());
angular.module("app").controller("NewsCtrl", NewsCtrl);
;
//# sourceMappingURL=NewsCtrl.js.map