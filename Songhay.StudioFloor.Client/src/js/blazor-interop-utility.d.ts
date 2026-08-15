export declare class BlazorInteropUtility {
    static getComputedStylePropertyValue(element: HTMLElement, propertyName: string): string | null;
    static getComputedStylePropertyValueById(elementId: string, propertyName: string): string | null;
    static getComputedStylePropertyValueByQuery(query: string, propertyName: string): string | null;
    static setComputedStylePropertyValue(element: HTMLElement, propertyName: string, propertyValue: string): void;
}
