var rx;
/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	// The require scope
/******/ 	var __webpack_require__ = {};
/******/ 	
/************************************************************************/
/******/ 	/* webpack/runtime/define property getters */
/******/ 	(() => {
/******/ 		// define getter functions for harmony exports
/******/ 		__webpack_require__.d = (exports, definition) => {
/******/ 			for(var key in definition) {
/******/ 				if(__webpack_require__.o(definition, key) && !__webpack_require__.o(exports, key)) {
/******/ 					Object.defineProperty(exports, key, { enumerable: true, get: definition[key] });
/******/ 				}
/******/ 			}
/******/ 		};
/******/ 	})();
/******/ 	
/******/ 	/* webpack/runtime/hasOwnProperty shorthand */
/******/ 	(() => {
/******/ 		__webpack_require__.o = (obj, prop) => (Object.prototype.hasOwnProperty.call(obj, prop))
/******/ 	})();
/******/ 	
/******/ 	/* webpack/runtime/make namespace object */
/******/ 	(() => {
/******/ 		// define __esModule on exports
/******/ 		__webpack_require__.r = (exports) => {
/******/ 			if(typeof Symbol !== 'undefined' && Symbol.toStringTag) {
/******/ 				Object.defineProperty(exports, Symbol.toStringTag, { value: 'Module' });
/******/ 			}
/******/ 			Object.defineProperty(exports, '__esModule', { value: true });
/******/ 		};
/******/ 	})();
/******/ 	
/************************************************************************/
var __webpack_exports__ = {};
// ESM COMPAT FLAG
__webpack_require__.r(__webpack_exports__);

// EXPORTS
__webpack_require__.d(__webpack_exports__, {
  BoleroUtility: () => (/* reexport */ BoleroUtility)
});

;// CONCATENATED MODULE: ./node_modules/songhay/core/models/window-animation.js
/**
 * Defines a wrapper
 * around {@link https://developer.mozilla.org/en-US/docs/Web/API/window/requestAnimationFrame |`window.requestAnimationFrame`}
 *
 * @see {@link https://stackblitz.com/edit/typescript-window-animation | `WindowAnimation` demo}
 *
 * @export
 * @class WindowAnimation
 */
class WindowAnimation {
    /**
     * calls {@link https://developer.mozilla.org/en-US/docs/Web/API/window/requestAnimationFrame |`window.requestAnimationFrame`} which will in turn call back to this member.
     *
     * @remarks
     *
     * {@link WindowAnimation.motionFunction} will be called,
     * throttled to the specified {@link WindowAnimation.fps}.
     *
     * @static
     * @param {DOMHighResTimeStamp} [currentTime]
     * @returns
     * @memberof WindowAnimation
     */
    static animate(currentTime) {
        var _a;
        if (!WindowAnimation.animationId) {
            return;
        }
        const animation = WindowAnimation.registry.get(WindowAnimation.animationId);
        if (!animation) {
            return;
        }
        if (animation.shouldStopAnimation) {
            return;
        }
        if (!currentTime) {
            currentTime = window.performance.now();
        }
        if (animation.isStartOfAnimation()) {
            animation.startTime = currentTime;
            animation.previousTime = animation.startTime;
        }
        animation.frameId = window.requestAnimationFrame(WindowAnimation.animate);
        animation.currentTime = currentTime;
        animation.delta = animation.currentTime - ((_a = animation.previousTime) !== null && _a !== void 0 ? _a : 0);
        if (animation.delta > animation.fpsInterval) {
            animation.previousTime = animation.currentTime - (animation.delta % animation.fpsInterval);
            animation.motionFunction(animation);
            ++animation.frameCount;
        }
    }
    /**
     * Cancels the animation associated with an instance of {@type WindowAnimation}.
     *
     * @static
     * @param {number} [animationId]
     * @returns {void}
     * @memberof WindowAnimation
     */
    static cancelAnimation(animationId) {
        const id = animationId !== null && animationId !== void 0 ? animationId : WindowAnimation.animationId;
        if (!id) {
            return;
        }
        const animation = WindowAnimation.registry.get(id);
        if (!animation) {
            return;
        }
        if (!animation.frameId) {
            return;
        }
        animation.shouldStopAnimation = true;
        window.cancelAnimationFrame(animation.frameId);
    }
    /**
     * Registers a new instance of {@type WindowAnimation}
     * in {@link WindowAnimation.registry} and returns it.
     *
     * @static
     * @param {number} fps
     * @param {(frameData: WindowAnimation) => void} motionFunction
     * @returns {WindowAnimation}
     * @memberof WindowAnimation
     */
    static registerAndGenerate(fps, motionFunction) {
        const animation = new WindowAnimation(fps, motionFunction);
        const nextId = WindowAnimation.registry.size + 1;
        animation.id = nextId;
        WindowAnimation.animationId = nextId;
        WindowAnimation.registry.set(nextId, animation);
        return animation;
    }
    /**
     * Creates an instance of @type {WindowAnimation}.
     * @param {number} fps
     * @param {(frameData: WindowAnimation) => void} motionFunction
     * @memberof WindowAnimation
     */
    constructor(fps, motionFunction) {
        if (Math.abs(fps) === 0) {
            fps = 1;
        }
        this.fps = fps;
        this.fpsInterval = 1000 / fps;
        this.frameCount = 0;
        this.motionFunction = motionFunction;
        this.delta = null;
        this.currentTime = null;
        this.frameId = null;
        this.id = null;
        this.previousTime = null;
        this.shouldStopAnimation = false;
        this.startTime = null;
    }
    /**
     * Measures the current window animation frames per second
     * based on {@link WindowAnimation.getElapsedTimeOfAnimation}.
     *
     * @returns {number}
     * @memberof WindowAnimation
     */
    getCurrentFps() {
        const duration = this.getElapsedTimeOfAnimation();
        if (!duration) {
            return 0;
        }
        if (!this.frameCount) {
            return 0;
        }
        return Math.round((1000 / (duration / this.frameCount)) * 100) / 100;
    }
    /**
     * Returns a diagnotic message
     * based on {@link WindowAnimation.getElapsedTimeOfAnimation}
     * and {@link WindowAnimation.getCurrentFps}.
     *
     * @returns {string}
     * @memberof WindowAnimation
     */
    getDiagnosticStatus() {
        const timeInSeconds = this.getElapsedTimeOfAnimationInSeconds();
        const currentFps = this.getCurrentFps();
        return `elapsed time: ${timeInSeconds} seconds @ ${currentFps} fps`;
    }
    /**
     * Returns {@link WindowAnimation.currentTime}
     * minus {@link WindowAnimation.startTime}.
     *
     * @returns {DOMHighResTimeStamp}
     * @memberof WindowAnimation
     */
    getElapsedTimeOfAnimation() {
        if (!this.currentTime) {
            return 0;
        }
        if (!this.startTime) {
            return 0;
        }
        return this.currentTime - this.startTime;
    }
    /**
     * Converts the result of {@link WindowAnimation.getElapsedTimeOfAnimation}
     * from milliseconds to seconds.
     *
     * @returns {DOMHighResTimeStamp}
     * @memberof WindowAnimation
     */
    getElapsedTimeOfAnimationInSeconds() {
        const timeInSeconds = Math.round((this.getElapsedTimeOfAnimation() / 1000) * 100) / 100;
        return timeInSeconds;
    }
    /**
     * Determines whether this instance is starting a new animation.
     *
     * @returns {boolean}
     * @memberof WindowAnimation
     */
    isStartOfAnimation() {
        return !this.startTime && !this.previousTime;
    }
}
/**
 * Registers instances of {@link WindowAnimation}
 * to be used with the {@link WindowAnimation.animate} method.
 *
 * @static
 * @type {Map<number, WindowAnimation>}
 * @memberof WindowAnimation
 */
WindowAnimation.registry = new Map();
//# sourceMappingURL=window-animation.js.map
;// CONCATENATED MODULE: ./node_modules/songhay/core/models/css-red-green-blue.js
/**
 * defines CSS RGB
 *
 * @export
 */
class CssRedGreenBlue {
    /**
     * Creates an instance of CssRedGreenBlue.
     */
    constructor(r, g, b) {
        this.r = r;
        this.g = g;
        this.b = b;
    }
}
//# sourceMappingURL=css-red-green-blue.js.map
;// CONCATENATED MODULE: ./node_modules/songhay/core/utilities/css.utility.js

/**
 * routines for inline CSS
 *
 * @export
 */
class CssUtility {
    /**
     * gets #ffffff format from 0xFFFFFF format
     */
    static getColorHex(color) {
        if (!color) {
            return null;
        }
        return color.replace('0x', '#').toLowerCase();
    }
    /**
     * gets r, g, b format from @type {CssRedGreenBlue} data
     */
    static getColorRgb(rgb) {
        if (!rgb) {
            return null;
        }
        return `${rgb.r}, ${rgb.g}, ${rgb.b}`;
    }
    /**
     * gets @type {CSSStyleDeclaration} data
     */
    static getComputedStylePropertyValue(element, propertyName) {
        if (!element) {
            return null;
        }
        if (!propertyName) {
            return null;
        }
        const elementStyle = window.getComputedStyle(element);
        return elementStyle.getPropertyValue(propertyName);
    }
    /**
     * gets @type {CSSStyleDeclaration} data
     */
    static getComputedStylePropertyValueById(elementId, propertyName) {
        if (!elementId) {
            return null;
        }
        if (!propertyName) {
            return null;
        }
        const element = window.document.getElementById(elementId);
        if (!element) {
            return null;
        }
        const elementStyle = window.getComputedStyle(element);
        return elementStyle.getPropertyValue(propertyName);
    }
    /**
     * gets @type {CSSStyleDeclaration} data
     */
    static getComputedStylePropertyValueByQuery(query, propertyName) {
        if (!query) {
            return null;
        }
        if (!propertyName) {
            return null;
        }
        const element = window.document.querySelector(query);
        if (!element) {
            return null;
        }
        const elementStyle = window.getComputedStyle(element);
        return elementStyle.getPropertyValue(propertyName);
    }
    /**
     * gets opacity 0.70 format from "70" format
     */
    static getOpacity(opacity) {
        return parseInt(opacity, 10) / 100;
    }
    /**
     * gets 70px pixel format from number 70
     */
    static getPixelValue(pixels) {
        return `${pixels}px`;
    }
    /**
     * gets @type {CssRedGreenBlue} data from #ffffff (or #fff) format
     *
     * @see http://stackoverflow.com/questions/5623838/rgb-to-hex-and-hex-to-rgb/5624139?stw=2#5624139
     */
    static getRgbFromHex(hex) {
        if (!hex) {
            return null;
        }
        // Expand shorthand form (e.g. "03F") to full form (e.g. "0033FF")
        const shorthandRegex = /^#?([a-f\d])([a-f\d])([a-f\d])$/i;
        hex = hex.replace(shorthandRegex, (r, g, b) => {
            return r + r + g + g + b + b;
        });
        const result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
        return result
            ? new CssRedGreenBlue(parseInt(result[1], 16), parseInt(result[2], 16), parseInt(result[3], 16))
            : null;
    }
    /**
     * get linear-gradient CSS from @type {CssLinearGradientData} data
     */
    static getTintedBackground(data) {
        if (!data) {
            return null;
        }
        const tintHex = this.getColorHex(data.backgroundColor);
        const tintColor = this.getColorRgb(this.getRgbFromHex(tintHex));
        const tintAlpha = data.alpha;
        const backgroundImage = this.getUri(data.backgroundImageUri);
        return `linear-gradient(rgba(${tintColor}, ${tintAlpha}), rgba(${tintColor}, ${tintAlpha})), ${backgroundImage}`;
    }
    /**
     * get url CSS from the URI
     */
    static getUri(uri) {
        return `url('${uri}')`;
    }
    /**
     * gets @type {CSSStyleDeclaration} data
     */
    static setComputedStylePropertyValue(element, propertyName, propertyValue) {
        if (!element) {
            return;
        }
        if (!propertyName) {
            return;
        }
        if (!propertyValue) {
            return;
        }
        element.style.setProperty(propertyName, propertyValue);
    }
}
//# sourceMappingURL=css.utility.js.map
;// CONCATENATED MODULE: ./node_modules/songhay/core/utilities/display-item.utility.js



/**
 * the default fallback display item grouping pair
 */
const DISPLAY_ITEM_GROUP_NONE = { id: 'zzz-group-none', displayText: '[no grouping]' };
/**
 * static routines for display-item models
 *
 * @export
 */
class DisplayItemUtility {
    /**
     * return the data to display a flat list of items
     * as nested groups
     */
    static displayInGroups(items, groupId, sortDescending = false) {
        DisplayItemUtility.setGrouping(items, groupId);
        const groups = DisplayItemUtility.group(items);
        return DisplayItemUtility.nestIntoGroups(groups, sortDescending);
    }
    /**
     * return the display item pair
     * from the selectable map
     */
    static getItemMapPair(item, groupId) {
        const doGroupIdWarning = (id) => {
            const message = [
                'The expected selectable map group display text is not here.',
                ` [ID: ${(id || '[missing]')}]`
            ].join('');
            console.warn(message);
        };
        const doGroupMapWarning = () => {
            console.warn('The expected item map is not here.', { item });
        };
        const getFirstPair = () => {
            if (!item.map || !item.map.size) {
                doGroupMapWarning();
                return DISPLAY_ITEM_GROUP_NONE;
            }
            const first = Array.from(item.map.entries())[0];
            const id = first[0];
            const displayText = first[1];
            if (!displayText) {
                doGroupIdWarning(id);
            }
            return { id, displayText };
        };
        const getPairWithId = (id) => {
            if (!item.map || !item.map.size) {
                doGroupMapWarning();
                return DISPLAY_ITEM_GROUP_NONE;
            }
            if (!item.map.has(id)) {
                id = Array
                    .from(item.map.keys())
                    .find(i => i.toString().startsWith(id));
                if (!id) {
                    console.warn('The expected item map identifier is not here.', { item });
                    return DISPLAY_ITEM_GROUP_NONE;
                }
            }
            const displayText = item.map.get(id);
            if (!displayText) {
                doGroupIdWarning(id);
            }
            return { id, displayText };
        };
        const pair = groupId ? getPairWithId(groupId) : getFirstPair();
        return pair;
    }
    /**
     * return items that can stringify as JSON
     */
    static getStringifiableObject(item) {
        const mo = (!item.map) ? {} : MapObjectUtility.getObject(item.map);
        return Object.assign(Object.assign(Object.assign(Object.assign(Object.assign({}, item), item), item), { map: mo }), { childItems: (!item.childItems || !item.childItems.length) ? [] : item.childItems.map(i => DisplayItemUtility.getStringifiableObject(i)) });
    }
    /**
     * groups the specified items
     * into an interim, anonymous object
     */
    static group(items) {
        if (!items) {
            throw new Error('The expected items are not here.');
        }
        const groups = ArrayUtility.groupBy(items, (i) => i.groupId);
        const reduced = ReducedGroupUtility.reduceToObject(groups);
        return reduced;
    }
    /**
     * nests the groups from `DisplayItemUtility.group`
     * for menu display; the groups are sorted by `groupId`
     */
    static nestIntoGroups(groups, sortDescending = false) {
        if (!groups) {
            throw new Error('The expected groups are not here.');
        }
        const sort = (keys, reverse) => {
            const sorted = keys.sort();
            return reverse ? sorted.reverse() : sorted;
        };
        const nested = sort(Object.keys(groups), sortDescending).map(i => {
            const group = groups[i];
            if (!group.length) {
                throw new Error('The expected grouping data format is not here.');
            }
            const first = group[0];
            if (!first.groupId) {
                throw new Error('The expected grouping identifier is not here.');
            }
            if (!first.displayText) {
                throw new Error('The expected grouping display text is not here.');
            }
            const menu = {
                id: first.groupId,
                displayText: first.groupDisplayText || '',
                childItems: ArrayUtility.sortItems(groups[i], 'sortOrdinal', false, sortDescending)
            };
            return menu;
        });
        return nested;
    }
    /**
     * sets menu-display grouping data
     * with a `Selectable.map` entry,
     * found by first position or the specified `groupId`.
     *
     * The `groupId` matches an entire `Selectable.map` key
     * or is a prefix, matching the first key.
     */
    static setGrouping(items, groupId) {
        if (!items) {
            throw new Error('The expected items are not here.');
        }
        const doGroupPairWarning = (item) => {
            const message = [
                'The expected Selectable map pair and/or groupId/displayText is not here.',
                ` [Group ID: ${(item.groupId || '[missing]')}]`,
                ` [Group Display Text: ${(item.displayText || '[missing]')}]`
            ].join('');
            console.warn(message, { item });
        };
        items
            .filter(i => i ? true : false)
            .forEach(i => {
            const pair = DisplayItemUtility.getItemMapPair(i, groupId);
            if (!pair) {
                if (!i.groupId || !i.displayText) {
                    doGroupPairWarning(i);
                }
            }
            else {
                i.groupId = pair.id;
                i.groupDisplayText = pair.displayText;
            }
        });
    }
    /**
     * return items as JSON
     */
    static stringify(item) {
        if (!item) {
            return null;
        }
        const stringifiable = DisplayItemUtility.getStringifiableObject(item);
        return JSON.stringify(stringifiable);
    }
    /**
     * return items as JSON
     */
    static stringifyAll(items) {
        if (!items) {
            return null;
        }
        const stringifiable = items.map(item => DisplayItemUtility.getStringifiableObject(item));
        return JSON.stringify(stringifiable);
    }
}
//# sourceMappingURL=display-item.utility.js.map
;// CONCATENATED MODULE: ./node_modules/songhay/core/utilities/dom.utility.js
/**
 * static members for DOM manipulation
 *
 * @export
 */
class DomUtility {
    /**
     * gets the child element, extending @type {HTMLElement}
     * from the specified query @type {string},
     */
    static getChildHtmlElementByQuery(element, query) {
        if (!element) {
            console.warn('the expected parent element is not here');
            return null;
        }
        return element.querySelector(query);
    }
    /**
     * gets the child elements, extending @type {HTMLElement}
     * from the specified query @type {string},
     */
    static getChildHtmlElementsByQuery(element, query) {
        if (!element) {
            console.warn('the expected parent element is not here');
            return null;
        }
        return element.querySelectorAll(query);
    }
    /**
     * gets the child elements, extending @type {HTMLElement}
     * from the specified query @type {string},
     */
    static getClosestHtmlElementByQuery(element, query) {
        if (!element) {
            console.warn('the expected parent element is not here');
            return null;
        }
        return element.closest(query);
    }
    /**
     * gets the element, extending @type {HTMLElement}
     * from the specified @type {ElementRef},
     * usually derived from @ViewChild
     */
    static getHtmlElement(elementRef) {
        const el = elementRef['nativeElement'];
        if (!el) {
            console.warn('the expected element is not here');
            return null;
        }
        return el;
    }
    /**
     * get an array of @type {Element}
     * from the specified collection
     */
    static getHtmlElements(collection) {
        const children = Array.from(collection);
        if (!children) {
            console.warn('the expected element children are not here');
            return [];
        }
        if (!children.length) {
            console.warn('the expected number of element children is not here');
            return [];
        }
        return children;
    }
    /**
     * gets the @type {HTMLHeadingElement}
     * from the specified heading level
     */
    static getHtmlHeadingElement(level = 0, windowDocument = document) {
        if (!windowDocument) {
            return null;
        }
        const heading = 0 < level && level < 7
            ? windowDocument.createElement(`h${level}`)
            : windowDocument.createElement('h2');
        return heading;
    }
    /**
     * gets the @type {CSSStyleDeclaration}
     * from the specified @type {Element}
     */
    static getStyleDeclaration(element) {
        if (!element) {
            return null;
        }
        const style = element['style'];
        if (!style) {
            console.warn('the expected CSS style declaration is not here');
            return null;
        }
        return style;
    }
    /**
     * returns an element extending @type {HTMLElement[]}
     * from the specified markup
     */
    static parseAsHtmlElement(markup, expectedElementName = 'div') {
        if (!markup) {
            return null;
        }
        const elements = DomUtility.parseAsHtmlElements(markup, expectedElementName);
        if (!elements.length) {
            return null;
        }
        return elements[0];
    }
    /**
     * returns an array of elements extending @type {HTMLElement[]}
     * from the specified markup
     */
    static parseAsHtmlElements(markup, expectedElementName) {
        if (!markup) {
            return [];
        }
        const parser = new DOMParser();
        const supportedType = 'text/xml';
        const localDocument = parser.parseFromString(markup, supportedType);
        const elements = localDocument.getElementsByTagName(expectedElementName);
        return Array.from(elements).map(e => e);
    }
    /**
     * runs the specified function of @type {() => void}
     * when `DOMContentLoaded` fires for the browser window
     */
    static runWhenElementEvent(element, eventName, f) {
        if (!f) {
            return;
        }
        element.addEventListener(eventName, (e) => f(e));
    }
    /**
     * runs the specified function of @type {() => void}
     * when `DOMContentLoaded` fires for the browser window
     */
    static runWhenWindowContentLoaded(f) {
        if (!f) {
            return;
        }
        window.addEventListener('DOMContentLoaded', (e) => f(e));
    }
    /**
     * returns a @type {Promise} that resolves
     * in the specified milliseconds
     */
    static timeout(ms) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }
}
//# sourceMappingURL=dom.utility.js.map
;// CONCATENATED MODULE: ./node_modules/songhay/core/index.js





/* utilities */








//# sourceMappingURL=index.js.map
;// CONCATENATED MODULE: ./src/bolero-utility.ts

class BoleroUtility {
}
BoleroUtility.css = CssUtility;
BoleroUtility.dom = DomUtility;
DomUtility.runWhenWindowContentLoaded(() => {
    console.info({ DotNet });
});

;// CONCATENATED MODULE: ./src/index.ts
/* utilities */


rx = __webpack_exports__;
/******/ })()
;