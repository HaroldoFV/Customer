# Build
FROM node:14-alpine as build-step
RUN mkdir -p /app
WORKDIR /app
COPY package.json /app
RUN npm install
COPY . /app
RUN npm run build -- --prod
 
# Runtime
FROM nginx:1.17.1-alpine as runtime-step
COPY --from=build-step /app/dist/frontend /usr/share/nginx/html
COPY ./nginx-custom.conf /etc/nginx/conf.d/default.conf