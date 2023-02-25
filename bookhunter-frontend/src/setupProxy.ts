const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
    "/weatherforecast",
];

export default function setApp(app: { use: (arg0: any) => void; }) {
    const appProxy = createProxyMiddleware(context, {
        target: 'https://localhost:7123',
        secure: false
    });

    app.use(appProxy);
};
