FROM gcc:latest

COPY . /Docker_proyecto

WORKDIR /Docker_proyecto/

RUN gcc -o un_hilo un_hilo.c -lm

CMD ./un_hilo