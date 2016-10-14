var path = require("path");
var webpack = require("webpack");

var cfg = {
  entry: [
    'webpack/hot/dev-server',
    'webpack-dev-server/client?http://localhost:8080',
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