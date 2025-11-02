FROM --platform=linux/amd64 ubuntu:focal

RUN apt-get update && \
    apt-get install -y libglu1 xvfb libxcursor1

WORKDIR /usr/src/app

COPY ./build/server .

RUN chmod +x /usr/src/app/jxserverlinux.x86_64

CMD ["./jxserverlinux.x86_64"]

EXPOSE 7777