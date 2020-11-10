// @ts-check

import { HistoryBackHelper } from "../HistoryBackHelper/HistoryBackHelper";
import { EventDelegator } from "../EventDelegator/EventDelegator";
import { MediaQueryMatcher } from "../MediaQueryMatcher/MediaQueryMatcher";
import { ThemeDetector } from "../ThemeDetector/ThemeDetector";

import DomHelpers from "dom-helpers";
import * as DomHelpersExt from "../DomHelpers/DomHelpers";

// @ts-ignore
window.Skclusive = {
  // @ts-ignore
  ...window.Skclusive,
  Script: {
    // @ts-ignore
    ...(window.Skclusive || {}).Script,
    DomHelpers: {
      ...DomHelpers,
      ...DomHelpersExt,
      EventDelegator,
      MediaQueryMatcher,
      HistoryBackHelper,
      ThemeDetector,
    },
  },
};
