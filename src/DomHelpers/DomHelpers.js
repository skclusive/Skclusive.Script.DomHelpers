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

window.Skclusive = {
    Script: {
        DomHelpers : {
            ...DomHelpers,
            repaint,
            setStyle,
            getStyle,
            addClasses,
            removeClasses,
            updateClasses
        }
    },
    ...window.Skclusive
};
