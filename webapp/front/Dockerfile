FROM node:16.17-buster-slim as build-runner

WORKDIR /tmp/app

COPY ./front/package.json .
COPY ./front/package-lock.json .
COPY ./front/src ./src


RUN npm install

#COPY ./front/.swcrc .
COPY ./front/tsconfig-app.json .


RUN npm run build

FROM node:18.14-buster-slim as prod-runner

ARG NODE_ENV=production
ENV NODE_ENV $NODE_ENV

WORKDIR /app

COPY --from=build-runner /tmp/app/package.json /app/package.json
COPY --from=build-runner /tmp/app/package-lock.json /app/package-lock.json

COPY --from=build-runner /tmp/app/build /app/build

#RUN npm ci --omit=dev && npm cache clean --force

CMD ["node", "build/main.js"]
