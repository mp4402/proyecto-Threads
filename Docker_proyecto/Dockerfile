FROM gcc:latest

COPY . /Docker_proyecto

WORKDIR /Docker_proyecto/

RUN gcc -o cuatro_hilos cuatro_hilos.c -lm

CMD ./cuatro_hilos

#docker run --cpus="1.0" --memory="1g" -v C:/GitHub/proyecto-Threads/Docker_proyecto/so_respuesta:/Docker_proyecto/so_respuesta un-hilo
#docker run --cpus="1.0" --memory="1g" -v /Users/carlosalvarado/Desktop/SO/proyecto-Threads/Docker_proyecto/so_respuesta:/Docker_proyecto/so_respuesta docker-proyecto-un-hilo