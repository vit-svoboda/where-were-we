import path from 'path';

export default {
    debug: true,
    entry: './src/index.js',
    target: 'web',
    output: {
        path: path.join(__dirname, "dist"),
        publicPath: '/',
        filename: './bundle.js'
    },
    devServer: {
        contentBase: './src'
    },
    module: {
        loaders: [
            { test: /\.js$/, loader: 'babel', include: path.join(__dirname, 'src')
            }
        ]
    }
};