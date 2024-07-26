FROM node AS build
WORKDIR /app

COPY package.json ./
RUN npm install

COPY . .

RUN npm run build

ARG API_URL
ENV API_URL=$API_URL

FROM nginx:alpine

COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
