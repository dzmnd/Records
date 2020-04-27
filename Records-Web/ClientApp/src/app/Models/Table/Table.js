"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Table = /** @class */ (function () {
    function Table() {
        this.placeholder = "No Data Available";
        this.height = "100%";
        this.data = [];
        this.layout = "fitColumns";
        this.pagination = "local";
        this.paginationSize = 25;
        this.columnMinWidth = 20;
        this.columns = [];
        this.ajaxURL = "";
        this.ajaxFiltering = true;
        this.ajaxSorting = true;
        this.footerElement = "";
    }
    return Table;
}());
exports.Table = Table;
//# sourceMappingURL=Table.js.map