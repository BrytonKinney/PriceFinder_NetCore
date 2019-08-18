var ProductModel = /** @class */ (function () {
    function ProductModel(item, price) {
        this.Item = item;
        this.Price = price;
    }
    return ProductModel;
}());
var Services;
(function (Services) {
    Services[Services["Steam"] = 0] = "Steam";
})(Services || (Services = {}));
var Api = /** @class */ (function () {
    function Api(service) {
        this.baseUrl = "https://localhost:44315";
        if (service === Services.Steam)
            this.url = "/Steam/";
        this.request = new XMLHttpRequest();
    }
    Api.prototype.Search = function (term, callback) {
        var searchEndpoint = this.url + encodeURI(term);
        this.request.addEventListener('loadend', function () {
            if (this.readyState === this.DONE) {
                var data = JSON.parse(this.responseText);
                var results = void 0;
                results = data.map(function (el) {
                    return new ProductModel(el.itemName, el.price);
                });
                callback(results);
            }
        });
        this.request.open("GET", searchEndpoint);
        this.request.send();
    };
    return Api;
}());
var DomManip = /** @class */ (function () {
    function DomManip(querySelector) {
        this.selectedElement = document.querySelector(querySelector);
    }
    DomManip.prototype.ReplaceChildren = function (childElementType, results) {
        var _this = this;
        this.selectedElement.innerHTML = "";
        results.forEach(function (value, index) {
            var childNode = document.createElement(childElementType);
            childNode.innerHTML = value.Item;
            _this.selectedElement.appendChild(childNode);
        });
    };
    return DomManip;
}());
var searchButton = document.getElementById('search');
searchButton.addEventListener('click', function (ev) {
    var searchQuery;
    searchQuery = document.querySelector("input#searchQuery");
    var api = new Api(Services.Steam);
    var callback;
    callback = function (result) {
        var list = new DomManip('#results');
        list.ReplaceChildren('li', result);
    };
    api.Search(searchQuery.value, callback);
});
//# sourceMappingURL=site.js.map