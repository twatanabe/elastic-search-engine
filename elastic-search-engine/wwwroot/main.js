(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["main"],{

/***/ "./src/$$_lazy_route_resource lazy recursive":
/*!**********************************************************!*\
  !*** ./src/$$_lazy_route_resource lazy namespace object ***!
  \**********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

function webpackEmptyAsyncContext(req) {
	// Here Promise.resolve().then() is used instead of new Promise() to prevent
	// uncaught exception popping up in devtools
	return Promise.resolve().then(function() {
		var e = new Error('Cannot find module "' + req + '".');
		e.code = 'MODULE_NOT_FOUND';
		throw e;
	});
}
webpackEmptyAsyncContext.keys = function() { return []; };
webpackEmptyAsyncContext.resolve = webpackEmptyAsyncContext;
module.exports = webpackEmptyAsyncContext;
webpackEmptyAsyncContext.id = "./src/$$_lazy_route_resource lazy recursive";

/***/ }),

/***/ "./src/app/app.component.html":
/*!************************************!*\
  !*** ./src/app/app.component.html ***!
  \************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<body ng-app=\"elasticsearch\">\n  <div class=\"container margin-top margin-bottom\">\n    <div class=\"row margin-bottom-small\">\n      <div class=\"col-md-12\">\n        <form #searchForm=\"ngForm\" (ngSubmit)=\"search()\">\n          <div class=\"input-group\">\n            <input class=\"form-control\" autofocus type=\"text\" placeholder=\"Search for...\" [(ngModel)]=\"query\" name=\"query\">\n            <span class=\"input-group-btn\">\n              <button class=\"btn btn-default btn-search\" type=\"submit\">\n                Search\n                <i class=\"glyphicon glyphicon-search\"></i>\n              </button>\n            </span>\n          </div>\n        </form>\n      </div>\n    </div>\n    <div class=\"row\" *ngIf=\"total != null\">\n      <div class=\"col-md-12\">\n        <!-- <div ng-show=\"suggested.length > 0\">\n          <h4>Did you mean?</h4>\n          <ul class=\"sugestion-list\">\n            <li ng-repeat=\"item in suggested\">\n              <a ng-click=\"\">{{item}}</a>\n            </li>\n          </ul>\n        </div> -->\n        <h4 *ngIf=\"total > 0\">Found\n          <b>{{total}}</b> posts in\n          <b>{{took}}</b> ms.</h4>\n        <!-- <h4 ng-show=\"message\">{{message}}</h4> -->\n      </div>\n    </div>\n    <div class=\"row\">\n        <div class=\"col-sm-2 col-xs-3\">\n            <div *ngIf=\"total > 0\">\n                <h4>Posts by tag</h4>\n                <ul class=\"category-list\">\n                    <li *ngFor=\"let tag of aggKeys\">\n                        <span ng-class=\"{'label-primary label': isActive(tag)}\">\n                            <a (click)=\"toggleFilters(tag)\">{{tag}}</a>\n                        </span><small class=\"pull-right\"> ({{getValue(tag)}})</small>\n                    </li>\n                </ul>\n            </div>\n        </div>\n      <div class=\"col-sm-9 col-sm-offset-1\">\n        <ng-template ngFor let-result [ngForOf]=\"results\" *ngIf=\"!isLoading\">\n          <div class=\"row margin-bottom-small\">\n            <div class=\"col-md-12\">\n              <div class=\"row\">\n                <div class=\"col-md-12\">\n                  <a (click)=\"selectPost(result.id)\">\n                    <h3>{{result.title}}</h3>\n                  </a>\n                </div>\n              </div>\n              <div class=\"row\">\n                <div class=\"col-md-12\">\n                  <p>{{result.body}}</p>\n                </div>\n              </div>\n              <div class=\"row\">\n                <div class=\"col-md-8\">\n                  <ng-template ngFor let-tag [ngForOf]=\"result.tags\">\n                    <span class=\"badge badge-primary\">{{tag}}\n                    </span>\n                  </ng-template>\n                </div>\n                <div class=\"col-md-4 text-right\">\n                  <span class=\"label label-info label-round\">{{result.creationDate}}</span>\n                </div>\n                <!-- <div class=\"col-md-4 text-right\">\n                <small>Votes:\n                  <span class=\"label label-info label-round\">{{item.Score}}</span>\n                </small>\n                <small>Answers:\n                  <span class=\"label label-info label-round\">{{item.AnswerCount}}</span>\n                </small>\n              </div> -->\n              </div>\n            </div>\n          </div>\n        </ng-template>\n      </div>\n    </div>\n  </div>\n</body>\n"

/***/ }),

/***/ "./src/app/app.component.scss":
/*!************************************!*\
  !*** ./src/app/app.component.scss ***!
  \************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".btn-search {\n  margin-left: 10px;\n  height: 50px; }\n\na {\n  cursor: pointer; }\n\n.badge {\n  padding: 5px;\n  margin-right: 3px; }\n\n.label-info a {\n  color: #fff; }\n\n.label-round {\n  border-radius: 50%;\n  padding-left: 7px;\n  padding-right: 7px; }\n\n.margin-top {\n  margin-top: 40px; }\n\n.margin-bottom {\n  margin-bottom: 40px; }\n\n.margin-bottom-small {\n  margin-bottom: 20px; }\n\n.category-list, .sugestion-list {\n  list-style: none;\n  padding-left: 0; }\n\n.category-list li {\n  margin-bottom: 5px; }\n\n.category-list small {\n  padding-top: 3px; }\n\n.category-list .badge-primary a {\n  color: #fff;\n  font-weight: normal;\n  font-size: 14px; }\n\n.sugestion-list {\n  margin-bottom: 25px;\n  margin-top: 10px; }\n\n.sugestion-list li {\n  display: inline-block;\n  padding-right: 15px; }\n"

/***/ }),

/***/ "./src/app/app.component.ts":
/*!**********************************!*\
  !*** ./src/app/app.component.ts ***!
  \**********************************/
/*! exports provided: AppComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppComponent", function() { return AppComponent; });
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var AppComponent = /** @class */ (function () {
    function AppComponent(_httpService) {
        this._httpService = _httpService;
        this.aggKeys = [];
    }
    AppComponent.prototype.ngOnInit = function () {
        this.item = 'item';
        this.items = new Array();
        this.items.forEach(function (item) {
            item.item.Title = 'title';
            item.Body = 'body';
            item.Score = 'score';
            item.AnswerCount = 3;
        });
        this.message = 'message';
        this.tag = 'tag';
        this.count = 1;
        this.total = 0;
        this.search();
        // this.suggest();
    };
    AppComponent.prototype.search = function () {
        var _this = this;
        this._httpService
            .get("/api/search/search?query=" + this.query)
            .subscribe(function (values) {
            _this.isLoading = false;
            _this.searchResults = values;
            _this.mapData();
        });
    };
    AppComponent.prototype.searchByCategory = function (tags) {
        var _this = this;
        var queryInfo = { query: '', categories: tags };
        this._httpService
            .get('/api/search/searchbycategory', { params: queryInfo })
            .subscribe(function (values) {
            _this.isLoading = false;
            _this.searchResults = values;
            _this.mapData();
        });
    };
    AppComponent.prototype.suggest = function () {
        var _this = this;
        this._httpService
            .get("/api/search/suggest?query=" + this.query)
            .subscribe(function (values) {
            _this.suggested = values;
        });
    };
    AppComponent.prototype.mapData = function () {
        this.total = this.searchResults.total;
        this.took = this.searchResults.searchMilliseconds;
        this.aggs = this.searchResults.aggregationsByTags;
        this.aggKeys = [];
        for (var key in this.aggs) {
            if (this.aggs.hasOwnProperty(key)) {
                this.aggKeys.push(key);
            }
        }
        this.results = this.searchResults.results;
    };
    AppComponent.prototype.getValue = function (key) {
        return this.aggs[key];
    };
    AppComponent.prototype.toggleFilters = function (tag) {
        this.isLoading = true;
        var index = this.aggKeys.indexOf(tag);
        var tags = [tag];
        this.searchByCategory(tags);
    };
    AppComponent.prototype.selectPost = function (id) {
        var _this = this;
        console.log(id);
        this.findPost(id).subscribe(function (response) {
            _this.post = response;
        });
    };
    AppComponent.prototype.findPost = function (id) {
        return this._httpService.get("/api/search/get?id=" + id);
    };
    AppComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
            selector: 'app-root',
            template: __webpack_require__(/*! ./app.component.html */ "./src/app/app.component.html"),
            styles: [__webpack_require__(/*! ./app.component.scss */ "./src/app/app.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpClient"]])
    ], AppComponent);
    return AppComponent;
}());



/***/ }),

/***/ "./src/app/app.module.ts":
/*!*******************************!*\
  !*** ./src/app/app.module.ts ***!
  \*******************************/
/*! exports provided: AppModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppModule", function() { return AppModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_platform_browser__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/platform-browser */ "./node_modules/@angular/platform-browser/fesm5/platform-browser.js");
/* harmony import */ var _app_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./app.component */ "./src/app/app.component.ts");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};





var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            declarations: [_app_component__WEBPACK_IMPORTED_MODULE_3__["AppComponent"]],
            imports: [
                _angular_platform_browser__WEBPACK_IMPORTED_MODULE_2__["BrowserModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormsModule"],
                _angular_common_http__WEBPACK_IMPORTED_MODULE_4__["HttpClientModule"]
            ],
            providers: [],
            bootstrap: [_app_component__WEBPACK_IMPORTED_MODULE_3__["AppComponent"]]
        })
    ], AppModule);
    return AppModule;
}());



/***/ }),

/***/ "./src/environments/environment.ts":
/*!*****************************************!*\
  !*** ./src/environments/environment.ts ***!
  \*****************************************/
/*! exports provided: environment */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "environment", function() { return environment; });
// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.
var environment = {
    production: false
};
/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.


/***/ }),

/***/ "./src/main.ts":
/*!*********************!*\
  !*** ./src/main.ts ***!
  \*********************/
/*! no exports provided */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_platform_browser_dynamic__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/platform-browser-dynamic */ "./node_modules/@angular/platform-browser-dynamic/fesm5/platform-browser-dynamic.js");
/* harmony import */ var _app_app_module__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./app/app.module */ "./src/app/app.module.ts");
/* harmony import */ var _environments_environment__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./environments/environment */ "./src/environments/environment.ts");




if (_environments_environment__WEBPACK_IMPORTED_MODULE_3__["environment"].production) {
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["enableProdMode"])();
}
Object(_angular_platform_browser_dynamic__WEBPACK_IMPORTED_MODULE_1__["platformBrowserDynamic"])().bootstrapModule(_app_app_module__WEBPACK_IMPORTED_MODULE_2__["AppModule"])
    .catch(function (err) { return console.log(err); });


/***/ }),

/***/ 0:
/*!***************************!*\
  !*** multi ./src/main.ts ***!
  \***************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! C:\Code\elastic-search-engine\ng-spa\src\main.ts */"./src/main.ts");


/***/ })

},[[0,"runtime","vendor"]]]);
//# sourceMappingURL=main.js.map