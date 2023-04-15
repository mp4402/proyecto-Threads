FROM gcc:latest

COPY . /DockerWorld

WORKDIR /DockerWorld/

RUN gcc -o DockerWorld DockerWorld.c

CMD ./DockerWorld