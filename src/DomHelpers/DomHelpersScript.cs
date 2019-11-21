using Microsoft.AspNetCore.Components.Rendering;
using Skclusive.Core.Component;

namespace Skclusive.Script.DomHelpers
{
    public class DomHelpersScript : StaticComponentBase
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "script");
            builder.AddContent(1,
            #region DomHelpers.js
            @"
            (function () {
            'use strict';

            function ownerDocument(node) {
              return node && node.ownerDocument || document;
            }

            /**
            * Return the actively focused element safely.
            *
            * @param doc the document to checl
            */

            function activeElement(doc) {
              if (doc === void 0) {
                doc = ownerDocument();
              }

              // Support: IE 9 only
              // IE9 throws an 'Unspecified error' accessing document.activeElement from an <iframe>
              try {
                var active = doc.activeElement; // IE11 returns a seemingly empty object in some cases when accessing
                // document.activeElement from an <iframe>

                if (!active || !active.nodeName) return null;
                return active;
              } catch (e) {
                /* ie throws if no active element */
                return doc.body;
              }
            }

            function hasClass(element, className) {
              if (element.classList) return !!className && element.classList.contains(className);
              return (' ' + (element.className.baseVal || element.className) + ' ').indexOf(' ' + className + ' ') !== -1;
            }

            function addClass(element, className) {
              if (element.classList) element.classList.add(className);else if (!hasClass(element, className)) if (typeof element.className === 'string') element.className = element.className + ' ' + className;else element.setAttribute('class', (element.className && element.className.baseVal || '') + ' ' + className);
            }

            var canUseDOM = !!(typeof window !== 'undefined' && window.document && window.document.createElement);

            /* eslint-disable no-return-assign */
            var optionsSupported = false;
            var onceSupported = false;

            try {
              var options = {
                get passive() {
                  return optionsSupported = true;
                },

                get once() {
                  // eslint-disable-next-line no-multi-assign
                  return onceSupported = optionsSupported = true;
                }

              };

              if (canUseDOM) {
                window.addEventListener('test', options, options);
                window.removeEventListener('test', options, true);
              }
            } catch (e) {
              /* */
            }

            /**
            * An `addEventListener` ponyfill, supports the `once` option
            */
            function addEventListener(node, eventName, handler, options) {
              if (options && typeof options !== 'boolean' && !onceSupported) {
                var once = options.once,
                    capture = options.capture;
                var wrappedHandler = handler;

                if (!onceSupported && once) {
                  wrappedHandler = handler.__once || function onceHandler(event) {
                    this.removeEventListener(eventName, onceHandler, capture);
                    handler.call(this, event);
                  };

                  handler.__once = wrappedHandler;
                }

                node.addEventListener(eventName, wrappedHandler, optionsSupported ? options : capture);
              }

              node.addEventListener(eventName, handler, options);
            }

            /* https://github.com/component/raf */
            var prev = new Date().getTime();

            function fallback(fn) {
              var curr = new Date().getTime();
              var ms = Math.max(0, 16 - (curr - prev));
              var handle = setTimeout(fn, ms);
              prev = curr;
              return handle;
            }

            var vendors = ['', 'webkit', 'moz', 'o', 'ms'];
            var cancelMethod = 'clearTimeout';
            var rafImpl = fallback; // eslint-disable-next-line import/no-mutable-exports

            var getKey = function getKey(vendor, k) {
              return vendor + (!vendor ? k : k[0].toUpperCase() + k.substr(1)) + 'AnimationFrame';
            };

            if (canUseDOM) {
              vendors.some(function (vendor) {
                var rafMethod = getKey(vendor, 'request');

                if (rafMethod in window) {
                  cancelMethod = getKey(vendor, 'cancel'); // @ts-ignore

                  rafImpl = function rafImpl(cb) {
                    return window[rafMethod](cb);
                  };
                }

                return !!rafImpl;
              });
            }

            var cancel = function cancel(id) {
              // @ts-ignore
              if (typeof window[cancelMethod] === 'function') window[cancelMethod](id);
            };
            var request = rafImpl;

            var matchesImpl;
            function matches(node, selector) {
              if (!matchesImpl) {
                var body = document.body;
                var nativeMatch = body.matches || body.matchesSelector || body.webkitMatchesSelector || body.mozMatchesSelector || body.msMatchesSelector;

                matchesImpl = function matchesImpl(n, s) {
                  return nativeMatch.call(n, s);
                };
              }

              return matchesImpl(node, selector);
            }

            function closest(node, selector, stopAt) {
              if (node.closest && !stopAt) node.closest(selector);
              var nextNode = node;

              do {
                if (matches(nextNode, selector)) return nextNode;
                nextNode = nextNode.parentElement;
              } while (nextNode && nextNode !== stopAt && nextNode.nodeType === document.ELEMENT_NODE);

              return null;
            }

            /* eslint-disable no-bitwise, no-cond-assign */
            // HTML DOM and SVG DOM may have different support levels,
            // so we need to check on context instead of a document root element.
            function contains(context, node) {
              if (context.contains) return context.contains(node);
              if (context.compareDocumentPosition) return context === node || !!(context.compareDocumentPosition(node) & 16);
            }

            function ownerWindow(node) {
              var doc = ownerDocument(node);
              return doc && doc.defaultView || window;
            }

            function getComputedStyle(node, psuedoElement) {
              return ownerWindow(node).getComputedStyle(node, psuedoElement);
            }

            var rUpper = /([A-Z])/g;
            function hyphenate(string) {
              return string.replace(rUpper, '-$1').toLowerCase();
            }

            /**
            * Copyright 2013-2014, Facebook, Inc.
            * All rights reserved.
            * https://github.com/facebook/react/blob/2aeb8a2a6beb00617a4217f7f8284924fa2ad819/src/vendor/core/hyphenateStyleName.js
            */
            var msPattern = /^ms-/;
            function hyphenateStyleName(string) {
              return hyphenate(string).replace(msPattern, '-ms-');
            }

            var supportedTransforms = /^((translate|rotate|scale)(X|Y|Z|3d)?|matrix(3d)?|perspective|skew(X|Y)?)$/i;
            function isTransform(value) {
              return !!(value && supportedTransforms.test(value));
            }

            function style(node, property) {
              var css = '';
              var transforms = '';

              if (typeof property === 'string') {
                return node.style.getPropertyValue(hyphenateStyleName(property)) || getComputedStyle(node).getPropertyValue(hyphenateStyleName(property));
              }

              Object.keys(property).forEach(function (key) {
                var value = property[key];

                if (!value && value !== 0) {
                  node.style.removeProperty(hyphenateStyleName(key));
                } else if (isTransform(key)) {
                  transforms += key + '(' + value + ') ';
                } else {
                  css += hyphenateStyleName(key) + ': ' + value + ';';
                }
              });

              if (transforms) {
                css += 'transform: ' + transforms + ';';
              }

              node.style.cssText += ';' + css;
            }

            var toArray = Function.prototype.bind.call(Function.prototype.call, [].slice);
            function qsa(element, selector) {
              return toArray(element.querySelectorAll(selector));
            }

            function filterEvents(selector, handler) {
              return function filterHandler(e) {
                var top = e.currentTarget;
                var target = e.target;
                var matches = qsa(top, selector);
                if (matches.some(function (match) {
                  return contains(match, target);
                })) handler.call(this, e);
              };
            }

            function isDocument(element) {
              return 'nodeType' in element && element.nodeType === document.DOCUMENT_NODE;
            }

            function isWindow(node) {
              if ('window' in node && node.window === node) return node;
              if (isDocument(node)) return node.defaultView || false;
              return false;
            }

            function getscrollAccessor(offset) {
              var prop = offset === 'pageXOffset' ? 'scrollLeft' : 'scrollTop';

              function scrollAccessor(node, val) {
                var win = isWindow(node);

                if (val === undefined) {
                  return win ? win[offset] : node[prop];
                }

                if (win) {
                  win.scrollTo(win[offset], val);
                } else {
                  node[prop] = val;
                }
              }

              return scrollAccessor;
            }

            var scrollLeft = getscrollAccessor('pageXOffset');

            var scrollTop = getscrollAccessor('pageYOffset');

            function offset(node) {
              var doc = ownerDocument(node);
              var box = {
                top: 0,
                left: 0,
                height: 0,
                width: 0
              };
              var docElem = doc && doc.documentElement; // Make sure it's not a disconnected DOM node

              if (!docElem || !contains(docElem, node)) return box;
              if (node.getBoundingClientRect !== undefined) box = node.getBoundingClientRect();
              box = {
                top: box.top + scrollTop(node) - (docElem.clientTop || 0),
                left: box.left + scrollLeft(node) - (docElem.clientLeft || 0),
                width: box.width,
                height: box.height
              };
              return box;
            }

            function height(node, client) {
              var win = isWindow(node);
              return win ? win.innerHeight : client ? node.clientHeight : offset(node).height;
            }

            function removeEventListener(node, eventName, handler, options) {
              var capture = options && typeof options !== 'boolean' ? options.capture : options;
              node.removeEventListener(eventName, handler, capture);

              if (handler.__once) {
                node.removeEventListener(eventName, handler.__once, capture);
              }
            }

            function listen(node, eventName, handler, options) {
              addEventListener(node, eventName, handler, options);
              return function () {
                removeEventListener(node, eventName, handler, options);
              };
            }

            var isHTMLElement = function isHTMLElement(e) {
              return !!e && 'offsetParent' in e;
            };

            function offsetParent(node) {
              var doc = ownerDocument(node);
              var parent = node && node.offsetParent;

              while (isHTMLElement(parent) && parent.nodeName !== 'HTML' && style(parent, 'position') === 'static') {
                parent = parent.offsetParent;
              }

              return parent || doc.documentElement;
            }

            function _extends() {
              _extends = Object.assign || function (target) {
                for (var i = 1; i < arguments.length; i++) {
                  var source = arguments[i];

                  for (var key in source) {
                    if (Object.prototype.hasOwnProperty.call(source, key)) {
                      target[key] = source[key];
                    }
                  }
                }

                return target;
              };

              return _extends.apply(this, arguments);
            }

            var nodeName = function nodeName(node) {
              return node.nodeName && node.nodeName.toLowerCase();
            };

            function position(node, offsetParent$1) {
              var parentOffset = {
                top: 0,
                left: 0
              };
              var offset$1; // Fixed elements are offset from window (parentOffset = {top:0, left: 0},
              // because it is its only offset parent

              if (style(node, 'position') === 'fixed') {
                offset$1 = node.getBoundingClientRect();
              } else {
                var parent = offsetParent$1 || offsetParent(node);
                offset$1 = offset(node);
                if (nodeName(parent) !== 'html') parentOffset = offset(parent);
                var borderTop = String(style(parent, 'borderTopWidth') || 0);
                parentOffset.top += parseInt(borderTop, 10) - scrollTop(parent) || 0;
                var borderLeft = String(style(parent, 'borderLeftWidth') || 0);
                parentOffset.left += parseInt(borderLeft, 10) - scrollLeft(parent) || 0;
              }

              var marginTop = String(style(node, 'marginTop') || 0);
              var marginLeft = String(style(node, 'marginLeft') || 0); // Subtract parent offsets and node margins

              return _extends({}, offset$1, {
                top: offset$1.top - parentOffset.top - (parseInt(marginTop, 10) || 0),
                left: offset$1.left - parentOffset.left - (parseInt(marginLeft, 10) || 0)
              });
            }

            function replaceClassName(origClass, classToRemove) {
              return origClass.replace(new RegExp('(^|\\s)' + classToRemove + '(?:\\s|$)', 'g'), '$1').replace(/\s+/g, ' ').replace(/^\s*|\s*$/g, '');
            }

            function removeClass(element, className) {
              if (element.classList) {
                element.classList.remove(className);
              } else if (typeof element.className === 'string') {
                element.className = replaceClassName(element.className, className);
              } else {
                element.setAttribute('class', replaceClassName(element.className && element.className.baseVal || '', className));
              }
            }

            /* eslint-disable no-cond-assign, no-continue */
            /**
            * Find the first scrollable parent of an element.
            *
            * @param element Starting element
            * @param firstPossible Stop at the first scrollable parent, even if it's not currently scrollable
            */

            function scrollPrarent(element, firstPossible) {
              var position = style(element, 'position');
              var excludeStatic = position === 'absolute';
              var ownerDoc = element.ownerDocument;
              if (position === 'fixed') return ownerDoc || document; // @ts-ignore

              while ((element = element.parentNode) && !isDocument(element)) {
                var isStatic = excludeStatic && style(element, 'position') === 'static';
                var style$1 = (style(element, 'overflow') || '') + (style(element, 'overflow-y') || '') + style(element, 'overflow-x');
                if (isStatic) continue;

                if (/(auto|scroll)/.test(style$1) && (firstPossible || height(element) < element.scrollHeight)) {
                  return element;
                }
              }

              return ownerDoc || document;
            }

            function toggleClass(element, className) {
              if (element.classList) element.classList.toggle(className);else if (hasClass(element, className)) removeClass(element, className);else addClass(element, className);
            }

            function getWidth(node, client) {
              var win = isWindow(node);
              return win ? win.innerWidth : client ? node.clientWidth : offset(node).width;
            }

            var index = {
              addEventListener: addEventListener,
              removeEventListener: removeEventListener,
              filter: filterEvents,
              listen: listen,
              style: style,
              activeElement: activeElement,
              ownerDocument: ownerDocument,
              ownerWindow: ownerWindow,
              requestAnimationFrame: request,
              cancelAnimationFrame: cancel,
              matches: matches,
              height: height,
              width: getWidth,
              offset: offset,
              offsetParent: offsetParent,
              position: position,
              contains: contains,
              scrollParent: scrollPrarent,
              scrollTop: scrollTop,
              querySelectorAll: qsa,
              closest: closest,
              addClass: addClass,
              removeClass: removeClass,
              hasClass: hasClass,
              toggleClass: toggleClass
            };

            var DomHelpers = /*#__PURE__*/Object.freeze({
              __proto__: null,
              addEventListener: addEventListener,
              removeEventListener: removeEventListener,
              filter: filterEvents,
              listen: listen,
              style: style,
              activeElement: activeElement,
              ownerDocument: ownerDocument,
              ownerWindow: ownerWindow,
              requestAnimationFrame: request,
              cancelAnimationFrame: cancel,
              matches: matches,
              height: height,
              width: getWidth,
              offset: offset,
              offsetParent: offsetParent,
              position: position,
              contains: contains,
              scrollParent: scrollPrarent,
              scrollTop: scrollTop,
              querySelectorAll: qsa,
              closest: closest,
              addClass: addClass,
              removeClass: removeClass,
              hasClass: hasClass,
              toggleClass: toggleClass,
              'default': index
            });

            function repaint(element) {
              // This is for to force a repaint,
              // which is necessary in order to transition styles when adding a class name.
              element.scrollTop;
            }

            function setStyle(element, styles, trigger) {
              style(element, styles);
              if (trigger) {
                repaint(elememt);
              }
            }

            function getStyle(element, style$1) {
              return style(element, style$1);
            }

            function addClasses(element, classes, trigger) {
              classes.forEach(clazz => addClass(element, clazz));
              if (trigger) {
                repaint(elememt);
              }
            }

            function removeClasses(element, classes) {
              classes.forEach(clazz => removeClass(element, clazz));
            }

            function updateClasses(element, removes, adds, trigger) {
              removeClasses(element, removes);
              addClasses(element, adds);
              if (trigger) {
                repaint(elememt);
              }
            }

            function focus(element) {
              if (element) {
                setTimeout(() => element.focus(), 100);
              }
            }

            function blur(element) {
              if (element) {
                setTimeout(() => element.blur(), 1);
              }
            }

            function moveContent(source, target) {
              if (target && source) {
                target.innerHTML = '';
                while (source.childNodes.length > 0) {
                  target.appendChild(source.childNodes[0]);
                }
              }
            }

            function clearContent(element) {
              while (element.childNodes.length > 0) {
                element.removeChild(element.childNodes[0]);
              }
            }

            window.Skclusive = {
              ...window.Skclusive,
              Script: {
                ...(window.Skclusive || {}).Script,
                DomHelpers: {
                  ...DomHelpers,
                  repaint,
                  setStyle,
                  getStyle,
                  addClasses,
                  removeClasses,
                  updateClasses,
                  focus,
                  blur,
                  moveContent,
                  clearContent
                }
              }
            };

          }());
            "
            #endregion
            );
            builder.CloseElement();
        }
    }
}
