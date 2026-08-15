import { CssUtility, DomUtility } from 'songhay';

export class BoleroUtility {
    static getComputedStylePropertyValue(element: HTMLElement, propertyName: string): string | null {
        return CssUtility.getComputedStylePropertyValue(element, propertyName);
    }

    static getDomUtil(): DomUtility {
        return DomUtility;
    }
}
