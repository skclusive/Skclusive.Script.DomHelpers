import resolve from "rollup-plugin-node-resolve";
import { terser } from "rollup-plugin-terser";

process.env.INCLUDE_DEPS === "true";

module.exports = {
  input: "DomHelpers.js",
  output: {
    file: "artifacts/DomHelpers.js",
    format: "iife"
  },
  plugins: [resolve(), terser()]
};
