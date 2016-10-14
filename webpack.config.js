var path = require("path");
var webpack = require("webpack");

var cfg = {
  entry: [
    "./temp/main.js"
  ],
  output: {
    filename: "bundle.js"
  },
  plugins: [
    new webpack.DefinePlugin({
      'process.env': {
        'NODE_ENV': JSON.stringify('production')
      }
    })
  ]
};

module.exports = cfg;