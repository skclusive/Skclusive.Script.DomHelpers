import resolve from "rollup-plugin-node-resolve";

process.env.INCLUDE_DEPS === "true";
module.exports = {
  input: "DomHelpers.js",
  output: {
    file: "wwwroot/DomHelpers.js",
    format: "iife"
  },
  plugins: [resolve()]
};
