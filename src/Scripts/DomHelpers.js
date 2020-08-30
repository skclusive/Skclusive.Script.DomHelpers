// @ts-check

import * as DomHelpers from "dom-helpers";

function generateId() {
  return Math.random().toString(36).substr(2);
}

function debounce(func, wait = 166) {
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

function removeNode(node) {
  if (node && node.parentNode) {
    node.parentNode.removeChild(node);
  }
}

function goBack(depth) {
  setTimeout(() => history.go(depth), 2000);
}

const nextReferenceCaptureId = (function () {
  let referenceCaptureId = 10000;
  return () => referenceCaptureId++;
})();

function applyCaptureIdToElement(element, referenceCaptureId) {
  element.setAttribute(getCaptureIdAttributeName(referenceCaptureId), "");
}

function getCaptureIdAttributeName(referenceCaptureId) {
  return `_bl_${referenceCaptureId}`;
}

function captureNodeReturn(element) {
  if (element) {
    const referenceCaptureId = nextReferenceCaptureId();
    applyCaptureIdToElement(element, referenceCaptureId);
    return `${referenceCaptureId}`;
  }
  return null;
}

function activeElement(node) {
  return captureNodeReturn(
    DomHelpers.activeElement(DomHelpers.ownerDocument(node))
  );
}

function closest(node, selector, stopAt) {
  return captureNodeReturn(DomHelpers.closest(node, selector, stopAt));
}

function offsetParent(node) {
  return captureNodeReturn(DomHelpers.offsetParent(node));
}

function repaint(element) {
  // This is for to force a repaint,
  // which is necessary in order to transition styles when adding a class name.
  element.scrollTop;
}

function setStyle(element, styles, trigger) {
  DomHelpers.style(element, styles);
  if (trigger) {
    repaint(element);
  }
}

function getStyle(element, style) {
  return DomHelpers.style(element, style);
}

function addClasses(element, classes, trigger) {
  classes.forEach((clazz) => DomHelpers.addClass(element, clazz));
  if (trigger) {
    repaint(element);
  }
}

function removeClasses(element, classes) {
  classes.forEach((clazz) => DomHelpers.removeClass(element, clazz));
}

function updateClasses(element, removes, adds, trigger) {
  removeClasses(element, removes);
  addClasses(element, adds);
  if (trigger) {
    repaint(element);
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

function moveContent(source, target, targetBody, targetHead) {
  if (source) {
    let container = null;
    if (target) {
      container = target;
    } else if (targetHead) {
      container = DomHelpers.ownerDocument(targetHead).head;
    } else {
      container = DomHelpers.ownerDocument(targetBody).body;
    }
    while (source.childNodes.length > 0) {
      const child = source.childNodes[0];
      // ignoring comment node
      if (child.nodeType !== 8) {
        container.appendChild(child);
      } else {
        removeNode(child);
      }
    }
  }
}

function copyContent(source, target) {
  if (source && target) {
    target.innerHTML = source.innerHTML;
  }
}

function clearContent(element) {
  while (element.childNodes.length > 0) {
    element.removeChild(element.childNodes[0]);
  }
}

function getBoundry(element) {
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
function getScrollParent(parent, child) {
  let element = child;
  let scrollTop = 0;

  while (element && element !== parent) {
    element = element.parentNode;
    scrollTop += element.scrollTop;
  }
  return scrollTop;
}

function getElementOffset(element) {
  return {
    width: element.offsetWidth,
    height: element.offsetHeight,
  };
}

function getInputValue(input) {
  return input && input.value;
}

function getWindowOffset(element) {
  const containerWindow = DomHelpers.ownerWindow(element);
  return {
    width: containerWindow.innerWidth,
    height: containerWindow.innerHeight,
  };
}

function resetHeight(el) {
  if (!el) return;
  el.style.setProperty("height", "auto");
  const height = getComputedStyle(el).height;
  el.style.setProperty("height", "0");
  requestAnimationFrame(() => {
    el.style.setProperty("height", height);
  });
}

function toggleHeight(el) {
  if (!el) return;
  const height = getComputedStyle(el).height;
  el.style.setProperty("height", height);
  requestAnimationFrame(() => {
    el.style.setProperty("height", "0");
  });
}

// @ts-ignore
window.Skclusive = {
  // @ts-ignore
  ...window.Skclusive,
  Script: {
    // @ts-ignore
    ...(window.Skclusive || {}).Script,
    DomHelpers: {
      ...DomHelpers,
      generateId,
      activeElement,
      closest,
      offsetParent,
      repaint,
      setStyle,
      getStyle,
      addClasses,
      removeClasses,
      updateClasses,
      focus,
      blur,
      moveContent,
      copyContent,
      clearContent,
      getBoundry,
      getScrollParent,
      debounce,
      goBack,
      getElementOffset,
      getWindowOffset,
      getInputValue,
      removeNode,
      resetHeight,
      toggleHeight,
    },
  },
};
