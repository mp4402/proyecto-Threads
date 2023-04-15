FROM gcc:latest

COPY . /DockerWorld

WORKDIR /DockerWorld/

RUN gcc -o un_hilo un_hilo.c

CMD ./un_hilo