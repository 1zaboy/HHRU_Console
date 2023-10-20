FROM nginx:alpine

RUN mkdir -p /app

COPY ./dist/client-web/. /app

COPY ng-app.conf /etc/nginx/conf.d/
COPY nginx.conf /etc/nginx/nginx.conf

CMD ["nginx", "-g", "daemon off;"]
