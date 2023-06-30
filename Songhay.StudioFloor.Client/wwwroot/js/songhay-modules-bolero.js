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
  BoleroUtility: () => (/* reexport */ BoleroUtility),
  StudioFloorUtility: () => (/* reexport */ StudioFloorUtility)
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

;// CONCATENATED MODULE: ./node_modules/tslib/tslib.es6.mjs
/******************************************************************************
Copyright (c) Microsoft Corporation.

Permission to use, copy, modify, and/or distribute this software for any
purpose with or without fee is hereby granted.

THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES WITH
REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF MERCHANTABILITY
AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY SPECIAL, DIRECT,
INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES WHATSOEVER RESULTING FROM
LOSS OF USE, DATA OR PROFITS, WHETHER IN AN ACTION OF CONTRACT, NEGLIGENCE OR
OTHER TORTIOUS ACTION, ARISING OUT OF OR IN CONNECTION WITH THE USE OR
PERFORMANCE OF THIS SOFTWARE.
***************************************************************************** */
/* global Reflect, Promise */

var extendStatics = function(d, b) {
  extendStatics = Object.setPrototypeOf ||
      ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
      function (d, b) { for (var p in b) if (Object.prototype.hasOwnProperty.call(b, p)) d[p] = b[p]; };
  return extendStatics(d, b);
};

function __extends(d, b) {
  if (typeof b !== "function" && b !== null)
      throw new TypeError("Class extends value " + String(b) + " is not a constructor or null");
  extendStatics(d, b);
  function __() { this.constructor = d; }
  d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
}

var __assign = function() {
  __assign = Object.assign || function __assign(t) {
      for (var s, i = 1, n = arguments.length; i < n; i++) {
          s = arguments[i];
          for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p)) t[p] = s[p];
      }
      return t;
  }
  return __assign.apply(this, arguments);
}

function __rest(s, e) {
  var t = {};
  for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p) && e.indexOf(p) < 0)
      t[p] = s[p];
  if (s != null && typeof Object.getOwnPropertySymbols === "function")
      for (var i = 0, p = Object.getOwnPropertySymbols(s); i < p.length; i++) {
          if (e.indexOf(p[i]) < 0 && Object.prototype.propertyIsEnumerable.call(s, p[i]))
              t[p[i]] = s[p[i]];
      }
  return t;
}

function __decorate(decorators, target, key, desc) {
  var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
  if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
  else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
  return c > 3 && r && Object.defineProperty(target, key, r), r;
}

function __param(paramIndex, decorator) {
  return function (target, key) { decorator(target, key, paramIndex); }
}

function __esDecorate(ctor, descriptorIn, decorators, contextIn, initializers, extraInitializers) {
  function accept(f) { if (f !== void 0 && typeof f !== "function") throw new TypeError("Function expected"); return f; }
  var kind = contextIn.kind, key = kind === "getter" ? "get" : kind === "setter" ? "set" : "value";
  var target = !descriptorIn && ctor ? contextIn["static"] ? ctor : ctor.prototype : null;
  var descriptor = descriptorIn || (target ? Object.getOwnPropertyDescriptor(target, contextIn.name) : {});
  var _, done = false;
  for (var i = decorators.length - 1; i >= 0; i--) {
      var context = {};
      for (var p in contextIn) context[p] = p === "access" ? {} : contextIn[p];
      for (var p in contextIn.access) context.access[p] = contextIn.access[p];
      context.addInitializer = function (f) { if (done) throw new TypeError("Cannot add initializers after decoration has completed"); extraInitializers.push(accept(f || null)); };
      var result = (0, decorators[i])(kind === "accessor" ? { get: descriptor.get, set: descriptor.set } : descriptor[key], context);
      if (kind === "accessor") {
          if (result === void 0) continue;
          if (result === null || typeof result !== "object") throw new TypeError("Object expected");
          if (_ = accept(result.get)) descriptor.get = _;
          if (_ = accept(result.set)) descriptor.set = _;
          if (_ = accept(result.init)) initializers.unshift(_);
      }
      else if (_ = accept(result)) {
          if (kind === "field") initializers.unshift(_);
          else descriptor[key] = _;
      }
  }
  if (target) Object.defineProperty(target, contextIn.name, descriptor);
  done = true;
};

function __runInitializers(thisArg, initializers, value) {
  var useValue = arguments.length > 2;
  for (var i = 0; i < initializers.length; i++) {
      value = useValue ? initializers[i].call(thisArg, value) : initializers[i].call(thisArg);
  }
  return useValue ? value : void 0;
};

function __propKey(x) {
  return typeof x === "symbol" ? x : "".concat(x);
};

function __setFunctionName(f, name, prefix) {
  if (typeof name === "symbol") name = name.description ? "[".concat(name.description, "]") : "";
  return Object.defineProperty(f, "name", { configurable: true, value: prefix ? "".concat(prefix, " ", name) : name });
};

function __metadata(metadataKey, metadataValue) {
  if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(metadataKey, metadataValue);
}

function __awaiter(thisArg, _arguments, P, generator) {
  function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
  return new (P || (P = Promise))(function (resolve, reject) {
      function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
      function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
      function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
      step((generator = generator.apply(thisArg, _arguments || [])).next());
  });
}

function __generator(thisArg, body) {
  var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
  return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
  function verb(n) { return function (v) { return step([n, v]); }; }
  function step(op) {
      if (f) throw new TypeError("Generator is already executing.");
      while (g && (g = 0, op[0] && (_ = 0)), _) try {
          if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
          if (y = 0, t) op = [op[0] & 2, t.value];
          switch (op[0]) {
              case 0: case 1: t = op; break;
              case 4: _.label++; return { value: op[1], done: false };
              case 5: _.label++; y = op[1]; op = [0]; continue;
              case 7: op = _.ops.pop(); _.trys.pop(); continue;
              default:
                  if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                  if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                  if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                  if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                  if (t[2]) _.ops.pop();
                  _.trys.pop(); continue;
          }
          op = body.call(thisArg, _);
      } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
      if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
  }
}

var __createBinding = Object.create ? (function(o, m, k, k2) {
  if (k2 === undefined) k2 = k;
  var desc = Object.getOwnPropertyDescriptor(m, k);
  if (!desc || ("get" in desc ? !m.__esModule : desc.writable || desc.configurable)) {
      desc = { enumerable: true, get: function() { return m[k]; } };
  }
  Object.defineProperty(o, k2, desc);
}) : (function(o, m, k, k2) {
  if (k2 === undefined) k2 = k;
  o[k2] = m[k];
});

function __exportStar(m, o) {
  for (var p in m) if (p !== "default" && !Object.prototype.hasOwnProperty.call(o, p)) __createBinding(o, m, p);
}

function __values(o) {
  var s = typeof Symbol === "function" && Symbol.iterator, m = s && o[s], i = 0;
  if (m) return m.call(o);
  if (o && typeof o.length === "number") return {
      next: function () {
          if (o && i >= o.length) o = void 0;
          return { value: o && o[i++], done: !o };
      }
  };
  throw new TypeError(s ? "Object is not iterable." : "Symbol.iterator is not defined.");
}

function __read(o, n) {
  var m = typeof Symbol === "function" && o[Symbol.iterator];
  if (!m) return o;
  var i = m.call(o), r, ar = [], e;
  try {
      while ((n === void 0 || n-- > 0) && !(r = i.next()).done) ar.push(r.value);
  }
  catch (error) { e = { error: error }; }
  finally {
      try {
          if (r && !r.done && (m = i["return"])) m.call(i);
      }
      finally { if (e) throw e.error; }
  }
  return ar;
}

/** @deprecated */
function __spread() {
  for (var ar = [], i = 0; i < arguments.length; i++)
      ar = ar.concat(__read(arguments[i]));
  return ar;
}

/** @deprecated */
function __spreadArrays() {
  for (var s = 0, i = 0, il = arguments.length; i < il; i++) s += arguments[i].length;
  for (var r = Array(s), k = 0, i = 0; i < il; i++)
      for (var a = arguments[i], j = 0, jl = a.length; j < jl; j++, k++)
          r[k] = a[j];
  return r;
}

function __spreadArray(to, from, pack) {
  if (pack || arguments.length === 2) for (var i = 0, l = from.length, ar; i < l; i++) {
      if (ar || !(i in from)) {
          if (!ar) ar = Array.prototype.slice.call(from, 0, i);
          ar[i] = from[i];
      }
  }
  return to.concat(ar || Array.prototype.slice.call(from));
}

function __await(v) {
  return this instanceof __await ? (this.v = v, this) : new __await(v);
}

function __asyncGenerator(thisArg, _arguments, generator) {
  if (!Symbol.asyncIterator) throw new TypeError("Symbol.asyncIterator is not defined.");
  var g = generator.apply(thisArg, _arguments || []), i, q = [];
  return i = {}, verb("next"), verb("throw"), verb("return"), i[Symbol.asyncIterator] = function () { return this; }, i;
  function verb(n) { if (g[n]) i[n] = function (v) { return new Promise(function (a, b) { q.push([n, v, a, b]) > 1 || resume(n, v); }); }; }
  function resume(n, v) { try { step(g[n](v)); } catch (e) { settle(q[0][3], e); } }
  function step(r) { r.value instanceof __await ? Promise.resolve(r.value.v).then(fulfill, reject) : settle(q[0][2], r); }
  function fulfill(value) { resume("next", value); }
  function reject(value) { resume("throw", value); }
  function settle(f, v) { if (f(v), q.shift(), q.length) resume(q[0][0], q[0][1]); }
}

function __asyncDelegator(o) {
  var i, p;
  return i = {}, verb("next"), verb("throw", function (e) { throw e; }), verb("return"), i[Symbol.iterator] = function () { return this; }, i;
  function verb(n, f) { i[n] = o[n] ? function (v) { return (p = !p) ? { value: __await(o[n](v)), done: false } : f ? f(v) : v; } : f; }
}

function __asyncValues(o) {
  if (!Symbol.asyncIterator) throw new TypeError("Symbol.asyncIterator is not defined.");
  var m = o[Symbol.asyncIterator], i;
  return m ? m.call(o) : (o = typeof __values === "function" ? __values(o) : o[Symbol.iterator](), i = {}, verb("next"), verb("throw"), verb("return"), i[Symbol.asyncIterator] = function () { return this; }, i);
  function verb(n) { i[n] = o[n] && function (v) { return new Promise(function (resolve, reject) { v = o[n](v), settle(resolve, reject, v.done, v.value); }); }; }
  function settle(resolve, reject, d, v) { Promise.resolve(v).then(function(v) { resolve({ value: v, done: d }); }, reject); }
}

function __makeTemplateObject(cooked, raw) {
  if (Object.defineProperty) { Object.defineProperty(cooked, "raw", { value: raw }); } else { cooked.raw = raw; }
  return cooked;
};

var __setModuleDefault = Object.create ? (function(o, v) {
  Object.defineProperty(o, "default", { enumerable: true, value: v });
}) : function(o, v) {
  o["default"] = v;
};

function __importStar(mod) {
  if (mod && mod.__esModule) return mod;
  var result = {};
  if (mod != null) for (var k in mod) if (k !== "default" && Object.prototype.hasOwnProperty.call(mod, k)) __createBinding(result, mod, k);
  __setModuleDefault(result, mod);
  return result;
}

function __importDefault(mod) {
  return (mod && mod.__esModule) ? mod : { default: mod };
}

function __classPrivateFieldGet(receiver, state, kind, f) {
  if (kind === "a" && !f) throw new TypeError("Private accessor was defined without a getter");
  if (typeof state === "function" ? receiver !== state || !f : !state.has(receiver)) throw new TypeError("Cannot read private member from an object whose class did not declare it");
  return kind === "m" ? f : kind === "a" ? f.call(receiver) : f ? f.value : state.get(receiver);
}

function __classPrivateFieldSet(receiver, state, value, kind, f) {
  if (kind === "m") throw new TypeError("Private method is not writable");
  if (kind === "a" && !f) throw new TypeError("Private accessor was defined without a setter");
  if (typeof state === "function" ? receiver !== state || !f : !state.has(receiver)) throw new TypeError("Cannot write private member to an object whose class did not declare it");
  return (kind === "a" ? f.call(receiver, value) : f ? f.value = value : state.set(receiver, value)), value;
}

function __classPrivateFieldIn(state, receiver) {
  if (receiver === null || (typeof receiver !== "object" && typeof receiver !== "function")) throw new TypeError("Cannot use 'in' operator on non-object");
  return typeof state === "function" ? receiver === state : state.has(receiver);
}

/* harmony default export */ const tslib_es6 = ({
  __extends,
  __assign,
  __rest,
  __decorate,
  __param,
  __metadata,
  __awaiter,
  __generator,
  __createBinding,
  __exportStar,
  __values,
  __read,
  __spread,
  __spreadArrays,
  __spreadArray,
  __await,
  __asyncGenerator,
  __asyncDelegator,
  __asyncValues,
  __makeTemplateObject,
  __importStar,
  __importDefault,
  __classPrivateFieldGet,
  __classPrivateFieldSet,
  __classPrivateFieldIn,
});

;// CONCATENATED MODULE: ./src/studio-floor-utility.ts


class StudioFloorUtility {
    static runMyAnimation(instance) {
        WindowAnimation.registerAndGenerate(1, (animation) => __awaiter(this, void 0, void 0, function* () {
            const x = yield instance.invokeMethodAsync('getNextX');
            console.info(animation.getDiagnosticStatus());
            if (!x || x > 99) {
                animation.shouldStopAnimation = true;
            }
        }));
    }
}
DomUtility.runWhenWindowContentLoaded(() => {
    console.info('the `DotNet` “namespace” should not be undefined:', { DotNet });
    console.warn({ StudioFloorUtility }, { window });
});

;// CONCATENATED MODULE: ./src/_index.ts
/* utilities */



rx = __webpack_exports__;
/******/ })()
;