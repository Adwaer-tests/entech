FROM node:18.14-buster-slim as build-runner

WORKDIR /tmp/app

COPY ./src /tmp/app/src/
COPY ./angular.json /tmp/app/angular.json
COPY ./package.json /tmp/app/package.json
COPY ./package-lock.json /tmp/app/package-lock.json

COPY ./tsconfig.app.json /tmp/app/tsconfig.app.json
COPY ./tsconfig.json /tmp/app/tsconfig.json

RUN npm install

RUN npm run build

FROM node:18.14-buster-slim as prod-runner

ARG NODE_ENV=production
ENV NODE_ENV $NODE_ENV

WORKDIR /app

COPY --from=build-runner /tmp/app/package.json /app/package.json
COPY --from=build-runner /tmp/app/package-lock.json /app/package-lock.json

COPY --from=build-runner /tmp/app/dist/front /app/build

RUN npm ci --omit=dev && npm cache clean --force
RUN npm i -g http-server

WORKDIR build
CMD ["http-server", "-s"]
