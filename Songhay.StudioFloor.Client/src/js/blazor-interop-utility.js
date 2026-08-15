import { CssUtility } from 'songhay';
export class BlazorInteropUtility {
    static getComputedStylePropertyValue(element, propertyName) {
        return CssUtility.getComputedStylePropertyValue(element, propertyName);
    }
    static getComputedStylePropertyValueById(elementId, propertyName) {
        return CssUtility.getComputedStylePropertyValueById(elementId, propertyName);
    }
    static getComputedStylePropertyValueByQuery(query, propertyName) {
        return CssUtility.getComputedStylePropertyValueByQuery(query, propertyName);
    }
    static setComputedStylePropertyValue(element, propertyName, propertyValue) {
        CssUtility.setComputedStylePropertyValue(element, propertyName, propertyValue);
    }
}
//# sourceMappingURL=blazor-interop-utility.js.map