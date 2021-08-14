"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (Object.prototype.hasOwnProperty.call(b, p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
exports.destroySubjectMixin = void 0;
var rxjs_1 = require("rxjs");
var destroySubjectMixin = function (base) {
    if (base === void 0) { base = /** @class */ (function () {
        function class_1() {
        }
        return class_1;
    }()); }
    return /** @class */ (function (_super) {
        __extends(class_2, _super);
        function class_2() {
            var args = [];
            for (var _i = 0; _i < arguments.length; _i++) {
                args[_i] = arguments[_i];
            }
            var _this = _super.apply(this, args) || this;
            _this.destroy$ = new rxjs_1.Subject();
            var onDestroy = _this.ngOnDestroy;
            if (!onDestroy) {
                throw new Error("Class must implement OnDestroy");
            }
            _this.ngOnDestroy = function () {
                if (onDestroy) {
                    onDestroy.apply(_this);
                }
                _this.destroy$.next();
                _this.destroy$.complete();
            };
            return _this;
        }
        return class_2;
    }(base));
};
exports.destroySubjectMixin = destroySubjectMixin;
//# sourceMappingURL=destroy-subject.mixin.js.map