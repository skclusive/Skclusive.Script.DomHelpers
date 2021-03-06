﻿// @ts-check

import * as DomHelpers from "dom-helpers";

export function generateId() {
  return Math.random().toString(36).substr(2);
}

export function debounce(func, wait = 166) {
  let timeout;
  function debounced(...args) {
    // eslint-disable-next-line consistent-this
    const that = this;
    const later = () => {
      func.apply(that, args);
    };
    clearTimeout(timeout);
    timeout = setTimeout(later, wait);
  }

  debounced.clear = () => {
    clearTimeout(timeout);
  };

  return debounced;
}

export function removeNode(node) {
  if (node && node.parentNode) {
    node.parentNode.removeChild(node);
  }
}

export function goBack(depth) {
  setTimeout(() => history.go(depth), 2000);
}

const nextReferenceCaptureId = (function () {
  let referenceCaptureId = 10000;
  return () => referenceCaptureId++;
})();

export function applyCaptureIdToElement(element, referenceCaptureId) {
  element.setAttribute(getCaptureIdAttributeName(referenceCaptureId), "");
}

export function getCaptureIdAttributeName(referenceCaptureId) {
  return `_bl_${referenceCaptureId}`;
}

export function captureNodeReturn(element) {
  if (element) {
    const referenceCaptureId = nextReferenceCaptureId();
    applyCaptureIdToElement(element, referenceCaptureId);
    return `${referenceCaptureId}`;
  }
  return null;
}

export function activeElement(node) {
  return captureNodeReturn(
    DomHelpers.activeElement(DomHelpers.ownerDocument(node))
  );
}

export function closest(node, selector, stopAt) {
  return captureNodeReturn(DomHelpers.closest(node, selector, stopAt));
}

export function offsetParent(node) {
  return captureNodeReturn(DomHelpers.offsetParent(node));
}

export function repaint(element) {
  if (!element) return;
  // This is for to force a repaint,
  // which is necessary in order to transition styles when adding a class name.
  element.scrollTop;
}

export function setStyle(element, styles, trigger) {
  if (!element) return;
  DomHelpers.style(element, styles);
  if (trigger) {
    repaint(element);
  }
}

export function getStyle(element, style) {
  if (!element) return null;
  return DomHelpers.style(element, style);
}

export function addClasses(element, classes, trigger) {
  if (!element) return;
  classes.forEach((clazz) => DomHelpers.addClass(element, clazz));
  if (trigger) {
    repaint(element);
  }
}

export function removeClasses(element, classes) {
  if (!element) return;
  classes.forEach((clazz) => DomHelpers.removeClass(element, clazz));
}

export function updateClasses(element, removes, adds, trigger) {
  if (!element) return;
  removeClasses(element, removes);
  addClasses(element, adds);
  if (trigger) {
    repaint(element);
  }
}

export function focus(element) {
  if (element) {
    setTimeout(() => element.focus(), 100);
  }
}

export function blur(element) {
  if (element) {
    setTimeout(() => element.blur(), 1);
  }
}

export function moveContent(source, target, targetBody, targetHead) {
  if (source) {
    let container = null;
    let before = null;
    if (target) {
      container = target;
    } else if (targetHead) {
      container = DomHelpers.ownerDocument(targetHead).head;
      const noncomment = Array.from(source.childNodes).find(child => child.nodeType !== 8);
      before = noncomment && container.querySelector(noncomment.tagName);
    } else {
      container = DomHelpers.ownerDocument(targetBody).body;
    }
    while (source.childNodes.length > 0) {
      const child = source.childNodes[0];
      // ignoring comment node
      if (child.nodeType !== 8) {
        if (before) {
          container.insertBefore(child, before);
        } else {
          container.appendChild(child);
        }
      } else {
        removeNode(child);
      }
    }
  }
}

export function copyContent(source, target) {
  if (source && target) {
    target.innerHTML = source.innerHTML;
  }
}

export function clearContent(element) {
  if (!element) return;
  while (element.childNodes.length > 0) {
    element.removeChild(element.childNodes[0]);
  }
}

export function getBoundry(element) {
  if (!element) return;
  const boundry = element
    ? element.getBoundingClientRect()
    : {
        width: 0,
        height: 0,
        left: 0,
        top: 0,
      };
  return boundry;
}

// Sum the scrollTop between two elements.
export function getScrollParent(parent, child) {
  let element = child;
  let scrollTop = 0;

  while (element && element !== parent) {
    element = element.parentNode;
    scrollTop += element.scrollTop;
  }
  return scrollTop;
}

export function getElementOffset(element) {
  return {
    width: element.offsetWidth,
    height: element.offsetHeight,
  };
}

export function getInputValue(input) {
  return input && input.value;
}

export function getWindowOffset(element) {
  const containerWindow = DomHelpers.ownerWindow(element);
  return {
    width: containerWindow.innerWidth,
    height: containerWindow.innerHeight,
  };
}

export function resetHeight(el) {
  if (!el) return;
  el.style.setProperty("height", "auto");
  const height = getComputedStyle(el).height;
  el.style.setProperty("height", "0");
  requestAnimationFrame(() => {
    el.style.setProperty("height", height);
  });
}

export function toggleHeight(el) {
  if (!el) return;
  const height = getComputedStyle(el).height;
  el.style.setProperty("height", height);
  requestAnimationFrame(() => {
    el.style.setProperty("height", "0");
  });
}
