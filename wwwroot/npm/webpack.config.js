var webpack = require('webpack');

module.exports = {
    context: __dirname,
    entry: {
        Homepage: './react_modules/Homepage.js'
    },
    output: {
        path: __dirname + "./../js/dist",
        filename: "[name].bundle.js"
    },
    watch: true,
    mode: 'development',
    module: {
        rules: [
            {
                test: /\.jsx?$/,
                exclude: /(node_modules)/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: ['@babel/preset-env', '@babel/preset-react']
                    }
                }
            }
        ]
    },
    //node: {
    //    console: true,
    //    fs: 'empty',
    //    net: 'empty',
    //    tls: 'empty'
    //},
    plugins: [
        new webpack.DefinePlugin({
            LOAN_API: JSON.stringify('https://personalloancalculator.azurewebsites.net/api/loan'),
            //LOAN_API: JSON.stringify('http://localhost:62077/api/loan')
        })
    ]
}