#include <pthread.h>
#include <stdio.h>
#include <string.h>
#include <stdbool.h>
#include <stdlib.h>
#include <unistd.h>

#define MAX_THREADS 500
#define MAXCHAR 1000

pthread_t thread_id[MAX_THREADS];    

void * Open_file(void * data)
{
    FILE *fptr;
    char row[MAXCHAR];
    char *token;
    char c;
    printf("Hello from thread %u - I was created in iteration %d !\n", (int)pthread_self(), (int)data);
    fptr = fopen("example.csv","r");
    while (!feof(fptr)) {
        fgets(row,MAXCHAR,fptr);
        token = strtok(row,",");
        while(token!=NULL) {
            printf("Thread %u - created in ite: %d row: %s token: %s !\n", (int)pthread_self(), (int)data,row,token);
            token = strtok(NULL,",");
        }
    }
    fclose(fptr);
    pthread_exit(NULL);
}

int main(int argc, char * argv[])
{
    int rc, i, n;

    if(argc < 2) 
    {
        printf("Please add the number of threads to the command line\n");
        exit(1); 
    }
    n = atoi(argv[1]);
    if(n > MAX_THREADS) n = MAX_THREADS;

    for(i = 0; i < n; i++)
    {
        rc = pthread_create(&thread_id[i], NULL, Open_file, (void*)i);
        if(rc)
        {
             printf("\n ERROR: return code from pthread_create is %d \n", rc);
             exit(1);
        }
        printf("\n Thread %u. Created new thread (%u) in ite %d ...\n",(int)pthread_self(), (int)thread_id[i], i);
        //if(i % 5 == 0) sleep(1);
    }
    pthread_exit(NULL);
}