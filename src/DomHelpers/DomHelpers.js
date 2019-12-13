// @ts-check

import * as DomHelpers from "dom-helpers";

const nextReferenceCaptureId = (function() {
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
  classes.forEach(clazz => DomHelpers.addClass(element, clazz));
  if (trigger) {
    repaint(element);
  }
}

function removeClasses(element, classes) {
  classes.forEach(clazz => DomHelpers.removeClass(element, clazz));
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

function moveContent(source, target) {
  if (target && source) {
    target.innerHTML = "";
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

function getBoundry(element) {
  const boundry = element
    ? element.getBoundingClientRect()
    : {
        width: 0,
        height: 0,
        left: 0,
        top: 0
      };
  return boundry;
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
      clearContent,
      getBoundry
    }
  }
};
