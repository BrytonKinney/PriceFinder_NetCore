
class ProductModel {
    Item: string;
    Price: string;

    constructor(item: string, price: string) {
        this.Item = item;
        this.Price = price;
    }
}

interface ICallback {
    (result: ProductModel[]): void
}
enum Services {
    Steam
}

class Api {
    baseUrl: string;
    url: string;
    request: XMLHttpRequest;

    constructor(service: Services) {
        this.baseUrl = "https://localhost:44315";
        if (service === Services.Steam)
            this.url = "/Steam/";
        this.request = new XMLHttpRequest();
    }

    Search(term: string, callback: ICallback): void {
        let searchEndpoint = this.url + encodeURI(term);
        this.request.addEventListener('loadend', function () {
            if (this.readyState === this.DONE) {
                let data = JSON.parse(this.responseText);
                let results: ProductModel[];
                results = data.map((el) => {
                    return new ProductModel(el.itemName, el.price);
                });
                callback(results);
            }
        });
        this.request.open("GET", searchEndpoint);
        this.request.send();
    }
}

class DomManip {
    selectedElement: Element;

    constructor(querySelector: string) {
        this.selectedElement = document.querySelector(querySelector);
    }

    ReplaceChildren(childElementType: string, results: ProductModel[]): void {
        this.selectedElement.innerHTML = "";
        results.forEach((value: ProductModel, index: number) => {
            let childNode = document.createElement(childElementType);
            childNode.innerHTML = value.Item;
            this.selectedElement.appendChild(childNode);
        });
    }
}

let searchButton = document.getElementById('search');
searchButton.addEventListener('click', function (ev: MouseEvent) {
    let searchQuery: HTMLInputElement;
    searchQuery = document.querySelector("input#searchQuery");
    let api = new Api(Services.Steam);
    let callback: ICallback;
    callback = function (result: ProductModel[]): void {
        let list = new DomManip('#results');
        list.ReplaceChildren('li', result);
    };
    api.Search(searchQuery.value, callback);
});