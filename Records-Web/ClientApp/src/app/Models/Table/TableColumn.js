"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var TableColumn = /** @class */ (function () {
    function TableColumn() {
        this.title = "";
        this.field = "";
        this.editor = false;
        this.headerFilter = false;
        this.headerFilterParams = {};
        this.headerFilterPlaceholder = "filter...";
        this.formatter = "html";
        this.visible = true;
        this.headerSort = true;
        this.align = "center";
        this.validator = false;
        this.minWidth = 40;
        this.width = 40;
    }
    return TableColumn;
}());
exports.TableColumn = TableColumn;
//# sourceMappingURL=TableColumn.js.map