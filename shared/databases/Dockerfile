FROM alpine:latest

RUN apk add --no-cache sqlite

WORKDIR /db

COPY init.sql .

CMD ["sh", "-c", "sqlite3 todoverse.db < init.sql"]