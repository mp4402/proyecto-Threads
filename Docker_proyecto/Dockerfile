FROM gcc:latest

COPY . /Docker_proyecto

WORKDIR /Docker_proyecto/

RUN gcc -o un_hilo un_hilo.c -lm

CMD ./un_hilo

#docker run --cpus="1.0" --memory="1g" -v C:/GitHub/proyecto-Threads/Docker_proyecto/so_respuesta:/Docker_proyecto/so_respuesta un-hilo