#include <pthread.h>
#include <stdio.h>

int sum; /*las hebras comparten esta variable*/ 
void *runner(void *param); /* la hebra */

int main(int argc, char *argv[])
{
    pthread_t tid; /* el identificador de la hebra */
    pthread_attr_t attr; /* conjunto de atributos de la hebra */
    if (argc != 2) {
        fprintf (stderr, "uso: a.out <valor entero>\n"); 
        return -1;
    }
    if (strtol(argv[1])<0){
        fprintf(stderr, "%d debe ser >= 0 \n", strtol(argv[1]));
        return -1;
    }
    /* obtener los atributos predeterminados */ 
    pthread_attr_init(&attr);
    /* crear la hebra */
    pthread_create(&tid, &attr, runner, argv[1]); 
    /* esperar a que la hebra termine */ 
    pthread_join(tid,NULL);

    printf("sum = %d\n", sum);
}
/* La hebra inicia su ejecución en esta función */ 
void *runner(void *param)
{
    int i, upper = strtol(param);
    sum = 0;
    for (i = 1; i <= upper; i++){
        sum += i;
    } 
    pthread_exit(0);
}
