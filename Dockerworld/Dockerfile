FROM gcc:latest

COPY . /DockerWorld

WORKDIR /DockerWorld/

RUN gcc -o DockerWorld threads.c

CMD ./DockerWorld 3