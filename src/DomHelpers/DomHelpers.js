import * as DomHelpers from "dom-helpers";

function repaint(element) {
  // This is for to force a repaint,
  // which is necessary in order to transition styles when adding a class name.
  element.scrollTop;
}

function setStyle(element, styles, trigger) {
  DomHelpers.style(element, styles);
  if (trigger) {
    repaint(elememt);
  }
}

function getStyle(element, style) {
  return DomHelpers.style(element, style);
}

function addClasses(element, classes, trigger) {
  classes.forEach(clazz => DomHelpers.addClass(element, clazz));
  if (trigger) {
    repaint(elememt);
  }
}

function removeClasses(element, classes) {
  classes.forEach(clazz => DomHelpers.removeClass(element, clazz));
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

window.Skclusive = {
  Script: {
    DomHelpers: {
      ...DomHelpers,
      repaint,
      setStyle,
      getStyle,
      addClasses,
      removeClasses,
      updateClasses,
      focus,
      moveContent,
      clearContent
    },
    ...(window.Skclusive || {}).Script
  },
  ...window.Skclusive
};
