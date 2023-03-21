# Build stage
FROM node AS build

WORKDIR /app

COPY package*.json ./

RUN npm install
RUN npm install -g @angular/cli@v14-lts

COPY . .

RUN ng build

#
# CMD ["ng serve", "host 0.0.0.0"]
# CMD ["sh","-c", "sleep 1000"]


ENTRYPOINT ["sh", "-c", "ng serve --host 0.0.0.0"]


# # Serve stage
# FROM nginx:1.21-alpine
#
# COPY --from=build /app/dist/client /usr/share/nginx/html
#
# EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
