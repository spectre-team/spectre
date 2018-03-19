FROM nginx:1.13.8-alpine

## Remove default nginx website
RUN rm -rf /usr/share/nginx/html/*

COPY dist /usr/share/nginx/html

CMD ["nginx", "-g", "daemon off;"]
