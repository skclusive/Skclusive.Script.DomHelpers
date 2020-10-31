﻿using Skclusive.Core.Component;

namespace Skclusive.Script.DomHelpers
{
    public class DomHelpersScript : ScriptBase
    {
        public override string GetScript()
        {
            return @"!function(){""use strict"";function e(e,t,n){return t in e?Object.defineProperty(e,t,{value:n,enumerable:!0,configurable:!0,writable:!0}):e[t]=n,e}function t(e){return e&&e.ownerDocument||document}function n(e){void 0===e&&(e=t());try{var n=e.activeElement;return n&&n.nodeName?n:null}catch(t){return e.body}}function r(e,t){return e.classList?!!t&&e.classList.contains(t):-1!==("" ""+(e.className.baseVal||e.className)+"" "").indexOf("" ""+t+"" "")}function o(e,t){e.classList?e.classList.add(t):r(e,t)||(""string""==typeof e.className?e.className=e.className+"" ""+t:e.setAttribute(""class"",(e.className&&e.className.baseVal||"""")+"" ""+t))}var i=!(""undefined""==typeof window||!window.document||!window.document.createElement),c=!1,a=!1;try{var s={get passive(){return c=!0},get once(){return a=c=!0}};i&&(window.addEventListener(""test"",s,s),window.removeEventListener(""test"",s,!0))}catch(e){}function l(e,t,n,r){if(r&&""boolean""!=typeof r&&!a){var o=r.once,i=r.capture,s=n;!a&&o&&(s=n.__once||function e(r){this.removeEventListener(t,e,i),n.call(this,r)},n.__once=s),e.addEventListener(t,s,c?r:i)}e.addEventListener(t,n,r)}function u(e){var n=t(e);return n&&n.defaultView||window}function f(e,t){return u(e).getComputedStyle(e,t)}var d=/([A-Z])/g;function h(e){return e.replace(d,""-$1"").toLowerCase()}var m=/^ms-/;function p(e){return h(e).replace(m,""-ms-"")}var v=/^((translate|rotate|scale)(X|Y|Z|3d)?|matrix(3d)?|perspective|skew(X|Y)?)$/i;function g(e){return!(!e||!v.test(e))}function y(e,t){var n="""",r="""";if(""string""==typeof t)return e.style.getPropertyValue(p(t))||f(e).getPropertyValue(p(t));Object.keys(t).forEach((function(o){var i=t[o];i||0===i?g(o)?r+=o+""(""+i+"") "":n+=p(o)+"": ""+i+"";"":e.style.removeProperty(p(o))})),r&&(n+=""transform: ""+r+"";""),e.style.cssText+="";""+n}function w(e,t,n,r){var o=r&&""boolean""!=typeof r?r.capture:r;e.removeEventListener(t,n,o),n.__once&&e.removeEventListener(t,n.__once,o)}function b(e,t,n,r){return l(e,t,n,r),function(){w(e,t,n,r)}}function E(e,t,n){void 0===n&&(n=5);var r=!1,o=setTimeout((function(){r||function(e){var t=document.createEvent(""HTMLEvents"");t.initEvent(""transitionend"",!0,!0),e.dispatchEvent(t)}(e)}),t+n),i=b(e,""transitionend"",(function(){r=!0}),{once:!0});return function(){clearTimeout(o),i()}}function k(e,t,n,r){var o,i;null==n&&(o=y(e,""transitionDuration"")||"""",i=-1===o.indexOf(""ms"")?1e3:1,n=parseFloat(o)*i||0);var c=E(e,n,r),a=b(e,""transitionend"",t);return function(){c(),a()}}var L={transition:"""",""transition-duration"":"""",""transition-delay"":"""",""transition-timing-function"":""""};function T(e){var t=e.node,n=e.properties,r=e.duration,o=void 0===r?200:r,i=e.easing,c=e.callback,a=[],s={},l="""";Object.keys(n).forEach((function(e){var t=n[e];g(e)?l+=e+""(""+t+"") "":(s[e]=t,a.push(h(e)))})),l&&(s.transform=l,a.push(""transform"")),o>0&&(s.transition=a.join("", ""),s[""transition-duration""]=o/1e3+""s"",s[""transition-delay""]=""0s"",s[""transition-timing-function""]=i||""linear"");var u=k(t,(function(e){e.target===e.currentTarget&&(y(t,L),c&&c.call(this,e))}),o);return t.clientLeft,y(t,s),{cancel:function(){u(),y(t,L)}}}var N=(new Date).getTime();var C=""clearTimeout"",S=function(e){var t=(new Date).getTime(),n=Math.max(0,16-(t-N)),r=setTimeout(e,n);return N=t,r},O=function(e,t){return e+(e?t[0].toUpperCase()+t.substr(1):t)+""AnimationFrame""};i&&["""",""webkit"",""moz"",""o"",""ms""].some((function(e){var t=O(e,""request"");return t in window&&(C=O(e,""cancel""),S=function(e){return window[t](e)}),!!S}));var M,P=function(e){""function""==typeof window[C]&&window[C](e)},A=S;function D(e,t){if(!M){var n=document.body,r=n.matches||n.matchesSelector||n.webkitMatchesSelector||n.mozMatchesSelector||n.msMatchesSelector;M=function(e,t){return r.call(e,t)}}return M(e,t)}function _(e,t,n){e.closest&&!n&&e.closest(t);var r=e;do{if(D(r,t))return r;r=r.parentElement}while(r&&r!==n&&r.nodeType===document.ELEMENT_NODE);return null}function H(e,t){return e.contains?e.contains(t):e.compareDocumentPosition?e===t||!!(16&e.compareDocumentPosition(t)):void 0}var q=Function.prototype.bind.call(Function.prototype.call,[].slice);function x(e,t){return q(e.querySelectorAll(t))}function z(e){return""nodeType""in e&&e.nodeType===document.DOCUMENT_NODE}function j(e){return""window""in e&&e.window===e?e:z(e)&&e.defaultView||!1}function W(e){var t=""pageXOffset""===e?""scrollLeft"":""scrollTop"";return function(n,r){var o=j(n);if(void 0===r)return o?o[e]:n[t];o?o.scrollTo(o[e],r):n[t]=r}}var B=W(""pageXOffset""),F=W(""pageYOffset"");function I(e){var n=t(e),r={top:0,left:0,height:0,width:0},o=n&&n.documentElement;return o&&H(o,e)?(void 0!==e.getBoundingClientRect&&(r=e.getBoundingClientRect()),r={top:r.top+F(o)-(o.clientTop||0),left:r.left+B(o)-(o.clientLeft||0),width:r.width,height:r.height}):r}function V(e,t){var n=j(e);return n?n.innerHeight:t?e.clientHeight:I(e).height}function R(e){for(var n,r=t(e),o=e&&e.offsetParent;(n=o)&&""offsetParent""in n&&""HTML""!==o.nodeName&&""static""===y(o,""position"");)o=o.offsetParent;return o||r.documentElement}function $(){return($=Object.assign||function(e){for(var t=1;t<arguments.length;t++){var n=arguments[t];for(var r in n)Object.prototype.hasOwnProperty.call(n,r)&&(e[r]=n[r])}return e}).apply(this,arguments)}var X;function Y(e,t){return e.replace(new RegExp(""(^|\\s)""+t+""(?:\\s|$)"",""g""),""$1"").replace(/\s+/g,"" "").replace(/^\s*|\s*$/g,"""")}function U(e,t){e.classList?e.classList.remove(t):""string""==typeof e.className?e.className=Y(e.className,t):e.setAttribute(""class"",Y(e.className&&e.className.baseVal||"""",t))}function Z(e,t){var n=y(e,""position""),r=""absolute""===n,o=e.ownerDocument;if(""fixed""===n)return o||document;for(;(e=e.parentNode)&&!z(e);){var i=r&&""static""===y(e,""position""),c=(y(e,""overflow"")||"""")+(y(e,""overflow-y"")||"""")+y(e,""overflow-x"");if(!i&&(/(auto|scroll)/.test(c)&&(t||V(e)<e.scrollHeight)))return e}return o||document}var J={addEventListener:l,removeEventListener:w,animate:function(e,t,n,r,o){if(!(""nodeType""in e))return T(e);if(!t)throw new Error(""must include properties to animate"");return""function""==typeof r&&(o=r,r=""""),T({node:e,properties:t,duration:n,easing:r,callback:o})},filter:function(e,t){return function(n){var r=n.currentTarget,o=n.target;x(r,e).some((function(e){return H(e,o)}))&&t.call(this,n)}},listen:b,style:y,getComputedStyle:f,activeElement:n,ownerDocument:t,ownerWindow:u,requestAnimationFrame:A,cancelAnimationFrame:P,matches:D,height:V,width:function(e,t){var n=j(e);return n?n.innerWidth:t?e.clientWidth:I(e).width},offset:I,offsetParent:R,position:function(e,t){var n,r={top:0,left:0};if(""fixed""===y(e,""position""))n=e.getBoundingClientRect();else{var o=t||R(e);n=I(e),""html""!==function(e){return e.nodeName&&e.nodeName.toLowerCase()}(o)&&(r=I(o));var i=String(y(o,""borderTopWidth"")||0);r.top+=parseInt(i,10)-F(o)||0;var c=String(y(o,""borderLeftWidth"")||0);r.left+=parseInt(c,10)-B(o)||0}var a=String(y(e,""marginTop"")||0),s=String(y(e,""marginLeft"")||0);return $({},n,{top:n.top-r.top-(parseInt(a,10)||0),left:n.left-r.left-(parseInt(s,10)||0)})},contains:H,scrollbarSize:function(e){if((!X&&0!==X||e)&&i){var t=document.createElement(""div"");t.style.position=""absolute"",t.style.top=""-9999px"",t.style.width=""50px"",t.style.height=""50px"",t.style.overflow=""scroll"",document.body.appendChild(t),X=t.offsetWidth-t.clientWidth,document.body.removeChild(t)}return X},scrollLeft:B,scrollParent:Z,scrollTo:function(e,t){var n=I(e),r={top:0,left:0};if(e){var o=t||Z(e),i=j(o),c=F(o),a=V(o,!0);i||(r=I(o));var s=(n={top:n.top-r.top,left:n.left-r.left,height:n.height,width:n.width}).height,l=n.top+(i?0:c),u=l+s;c=c>l?l:u>c+a?u-a:c;var f=A((function(){return F(o,c)}));return function(){return P(f)}}},scrollTop:F,querySelectorAll:x,closest:_,addClass:o,removeClass:U,hasClass:r,toggleClass:function(e,t){e.classList?e.classList.toggle(t):r(e,t)?U(e,t):o(e,t)},transitionEnd:k};function Q(){return Math.random().toString(36).substr(2)}function G(e,t=166){let n;function r(...r){const o=this;clearTimeout(n),n=setTimeout(()=>{e.apply(o,r)},t)}return r.clear=()=>{clearTimeout(n)},r}function K(e){e&&e.parentNode&&e.parentNode.removeChild(e)}const ee=function(){let e=1e4;return()=>e++}();function te(e,t){e.setAttribute(ne(t),"""")}function ne(e){return""_bl_""+e}function re(e){if(e){const t=ee();return te(e,t),""""+t}return null}function oe(e){e&&e.scrollTop}function ie(e,t,n){e&&(t.forEach(t=>o(e,t)),n&&oe(e))}function ce(e,t){e&&t.forEach(t=>U(e,t))}var ae=Object.freeze({__proto__:null,generateId:Q,debounce:G,removeNode:K,goBack:function(e){setTimeout(()=>history.go(e),2e3)},applyCaptureIdToElement:te,getCaptureIdAttributeName:ne,captureNodeReturn:re,activeElement:function(e){return re(n(t(e)))},closest:function(e,t,n){return re(_(e,t,n))},offsetParent:function(e){return re(R(e))},repaint:oe,setStyle:function(e,t,n){e&&(y(e,t),n&&oe(e))},getStyle:function(e,t){return e?y(e,t):null},addClasses:ie,removeClasses:ce,updateClasses:function(e,t,n,r){e&&(ce(e,t),ie(e,n),r&&oe(e))},focus:function(e){e&&setTimeout(()=>e.focus(),100)},blur:function(e){e&&setTimeout(()=>e.blur(),1)},moveContent:function(e,n,r,o){if(e){let i=null,c=null;if(n)i=n;else if(o){i=t(o).head;const n=Array.from(e.childNodes).find(e=>8!==e.nodeType);c=n&&i.querySelector(n.tagName)}else i=t(r).body;for(;e.childNodes.length>0;){const t=e.childNodes[0];8!==t.nodeType?c?i.insertBefore(t,c):i.appendChild(t):K(t)}}},copyContent:function(e,t){e&&t&&(t.innerHTML=e.innerHTML)},clearContent:function(e){if(e)for(;e.childNodes.length>0;)e.removeChild(e.childNodes[0])},getBoundry:function(e){if(!e)return;return e?e.getBoundingClientRect():{width:0,height:0,left:0,top:0}},getScrollParent:function(e,t){let n=t,r=0;for(;n&&n!==e;)n=n.parentNode,r+=n.scrollTop;return r},getElementOffset:function(e){return{width:e.offsetWidth,height:e.offsetHeight}},getInputValue:function(e){return e&&e.value},getWindowOffset:function(e){const t=u(e);return{width:t.innerWidth,height:t.innerHeight}},resetHeight:function(e){if(!e)return;e.style.setProperty(""height"",""auto"");const t=getComputedStyle(e).height;e.style.setProperty(""height"",""0""),requestAnimationFrame(()=>{e.style.setProperty(""height"",t)})},toggleHeight:function(e){if(!e)return;const t=getComputedStyle(e).height;e.style.setProperty(""height"",t),requestAnimationFrame(()=>{e.style.setProperty(""height"",""0"")})}});class se{static construct(e,t,n){const r=Q(),o=se.callback(r);return se.cache[r]={id:r,node:e,name:t,delay:n,callback:o},se.initialize(r),r}static initialize(e){const t=se.cache[e];if(t){const{node:e,name:n,callback:r}=t;e.addEventListener(n,r)}}static callback(e){return t=>{t.preventDefault(),t.stopPropagation(),t.currentTarget.blur();const n=se.cache[e];n&&(n.delay?setTimeout(()=>history.back(),n.delay):history.back())}}static dispose(e){const t=se.cache[e];if(t&&t.callback){const{node:e,name:n,callback:r}=t;e.removeEventListener(n,r)}delete se.cache[e]}}e(se,""cache"",{});class le{static construct(e,t,n,r){const o=Q(),i=e instanceof Element?e:window;let c=le.callback(o);return r&&(c=G(c,r)),le.cache[o]={id:o,source:i,name:t,target:n,callback:c},le.initialize(o),o}static initialize(e){const t=le.cache[e];if(t){const{name:e,source:n,callback:r}=t;n.addEventListener(e,r)}}static callback(e){return t=>{const n=le.cache[e];n&&n.target&&n.target.invokeMethodAsync(""OnEventAsync"",JSON.stringify(t))}}static dispose(e){const t=le.cache[e];if(t&&t.callback){const{name:e,source:n,callback:r}=t;n.removeEventListener(e,r)}delete le.cache[e]}}e(le,""cache"",{});class ue{static construct(e,t){const n=Q();e=e.replace(/^@media( ?)/m,"""");const r=ue.callback(n);return ue.cache[n]={id:n,query:e,target:t,callback:r},ue.initialize(n),n}static initialize(e){const t=ue.cache[e];if(t){const{query:e,callback:n}=t,r=window.matchMedia(e);r.addEventListener(""change"",n),Object.assign(t,{queryList:r}),setTimeout(n)}}static callback(e){return t=>{const n=ue.cache[e];n&&n.target&&n.target.invokeMethodAsync(""OnChangeAsync"",!!n.queryList.matches)}}static dispose(e){const t=ue.cache[e];if(t&&t.callback){const{queryList:e,callback:n}=t;e.removeEventListener(""change"",n)}delete ue.cache[e]}}e(ue,""cache"",{});const fe={Dark:""(prefers-color-scheme: dark)"",Light:""(prefers-color-scheme: light)"",None:""(prefers-color-scheme: no-preference)""};class de{static construct(e){const t=Q(),n=de.callback(t);return de.cache[t]={id:t,target:e,callback:n},de.initialize(t),t}static initialize(e){const t=de.cache[e];if(t){const{callback:e}=t,n=[];Object.keys(fe).forEach(t=>{const r=window.matchMedia(fe[t]);r.addEventListener(""change"",e),n.push(r),setTimeout(()=>e(r))}),Object.assign(t,{activeMatches:n})}}static callback(e){return t=>{if(!t||!t.matches)return;const n=de.cache[e];if(n&&n.target){const e=Object.keys(fe);for(let r=0;r<e.length;r++){const o=e[r];if(t.media===fe[o]){n.target.invokeMethodAsync(""OnChangeAsync"",o);break}}}}}static dispose(e){const t=de.cache[e];if(t){const{callback:e,activeMatches:n}=t;n.forEach(t=>t.removeEventListener(""change"",e)),n.length=0}delete de.cache[e]}}e(de,""cache"",{}),window.Skclusive={...window.Skclusive,Script:{...(window.Skclusive||{}).Script,DomHelpers:{...J,...ae,EventDelegator:le,MediaQueryMatcher:ue,HistoryBackHelper:se,ThemeDetector:de}}}}();";
        }
    }
}