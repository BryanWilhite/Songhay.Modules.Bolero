import { CssUtility } from 'songhay';

export class BlazorInteropUtility {
    static getComputedStylePropertyValue(element: HTMLElement, propertyName: string): string | null {
        return CssUtility.getComputedStylePropertyValue(element, propertyName);
    }

    static getComputedStylePropertyValueById(elementId: string, propertyName: string): string | null {
        return CssUtility.getComputedStylePropertyValueById(elementId, propertyName);
    }

    static getComputedStylePropertyValueByQuery(query: string, propertyName: string): string | null {
        return CssUtility.getComputedStylePropertyValueByQuery(query, propertyName);
    }

    static setComputedStylePropertyValue(element: HTMLElement, propertyName: string, propertyValue: string): void {
        CssUtility.setComputedStylePropertyValue(element, propertyName, propertyValue);
    }
}
