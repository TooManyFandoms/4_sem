#include <stdio.h>
#include <stdlib.h>
#include <string.h>

void ToLowerCase(char *str)
{
    for (int i = 0; i < strlen(str); i++) {
		if (str[i] >= 'A' && str[i] <= 'Z') {
			str[i] = (str[i] - ('A'-'a'));
		}
	}
}

int Equal(char *x, char *y)
{
    if (x == NULL || y == NULL) {
        return 0;
    }
    return (strcmp(x, y) == 0);
}

int Bit(char *str, int value)
{
    if (value < 0) {
        value = 0;
    }
    return (str[value / 8] >> (7 - (value % 8))) & 1;
}

int FirstDifferentBit(char *x, char *y)
{
    if (x == 0 || y == 0) {
        return 0;
    }
    int i = 0;
    while (x[i] == y[i]) {
        i++;
    }
    i *= 8;
    while (Bit(x, i) == Bit(y, i)) {
        i++;
    }
    return i;
}

typedef struct TNode
{
    char *key;
    unsigned long long int value;
    int id;
    int bit;
    struct TNode *left;
    struct TNode *right;
} TNode;

void PatriciaNodeCreate(TNode *cur, int bit, char *key, unsigned long long int value, TNode *left, TNode *right)
{
    cur->id = -1;
    cur->bit = bit;
    if (key) {
        cur->key = (char *) malloc(strlen(key) + 1);
        strcpy(cur->key, key);
    } else {
        cur->key = key;
    }
    cur->value = value;
    cur->left = left;
    cur->right = right;
}

void PatriciaNodeDestroy(TNode *cur)
{
    free(cur->key);
    cur->key = NULL;
}

TNode *root = NULL;
int size = 0;

void Destroy(TNode *n)
{
    if (n->left->bit > n->bit) {
        Destroy(n->left);
    }
    if (n->right->bit > n->bit) {
        Destroy(n->right);
    }
    PatriciaNodeDestroy(n);
    free(n);
}

TNode *Find(char *key)
{
    TNode *x = root;
    TNode *y = root->left;
    while (x->bit < y->bit) {
        x = y;
        y = (Bit(key, y->bit) ? y->right : y->left);
    }
    if (!Equal(key, y->key)) {
        return NULL;
    }
    return y;
}

TNode *Insert(char *key, unsigned long long int value)
{
    TNode *x = root;
    TNode *y = root->left;
    while (x->bit < y->bit) {
        x = y;
        y = (Bit(key, y->bit) ? y->right : y->left);
    }
    if (Equal(key, y->key)) {
        return NULL;
    }
    int pos = FirstDifferentBit(key, y->key);
    x = root;
    TNode *z = root->left;
    while (x->bit < z->bit && z->bit < pos) {
        x = z;
        z = (Bit(key, z->bit) ? z->right : z->left);
    }
    y = (TNode *) malloc(sizeof(TNode));
    PatriciaNodeCreate(y, pos, key, value, Bit(key, pos) ? z : y, Bit(key, pos) ? y : z);
    if (Bit(key, x->bit)) {
        x->right = y;
    } else {
        x->left = y;
    }
    size++;
    return y;
}

void DataCopy(TNode *x, TNode *y)
{
    if (strlen(y->key) < strlen(x->key)) {
        free(y->key);
        y->key = (char *) malloc(strlen(x->key) + 1);
    }
    strcpy(y->key, x->key);
    y->value = x->value;
}

int Erace(char *key)
{
    TNode *x = root;
    TNode *y = x->left;
    TNode *z = NULL;
    while (x->bit < y->bit) {
        z = x;
        x = y;
        y = (Bit(key, y->bit)) ? y->right : y->left;
    }
    if (!Equal(key, y->key)) {
        return 0;
    }
    TNode *u;
    TNode *v;
    char *str;
    if (x != y) {
        DataCopy(x, y);
        str = x->key;
        v = x;
        u = (Bit(str, x->bit) ? x->right : x->left);
        while (v->bit < u->bit) {
            v = u;
            u = (Bit(str, u->bit) ? u->right : u->left);
        }
        if (Bit(str, v->bit)) {
            v->right = y;
        } else {
            v->left = y;
        }
    }
    TNode *w = (Bit(key, x->bit) ? x->left : x->right);
    if (Bit(key, z->bit)) {
        z->right = w;
    } else {
        z->left = w;
    }
    PatriciaNodeDestroy(x);
    free(x);
    size--;
    return 1;
}

int idx = 0;

void Indexate(TNode *n, TNode **v)
{
    n->id = idx;
    v[idx] = n;
    ++idx;
    if (n->left->bit > n->bit) {
        Indexate(n->left, v);
    }
    if (n->right->bit > n->bit) {
        Indexate(n->right, v);
    }
}

void Save(FILE *file)
{
    fwrite(&size, sizeof(int), 1, file);
    TNode **nodes = (TNode **) malloc(sizeof(TNode *) * (size + 1));
    idx = 0;
    Indexate(root, nodes);
    idx = 0;
    TNode *n;
    for (int i = 0; i < size + 1; i++) {
        n = nodes[i];
        fwrite(&n->value, sizeof(unsigned long long int), 1, file);
        fwrite(&n->bit, sizeof(int), 1, file);
        int sz;
        if (n->key) {
            sz = strlen(n->key);
        } else {
            sz = 0;
        }
        fwrite(&sz, sizeof(int), 1, file);
        fwrite(n->key, sz, 1, file);
        fwrite(&n->left->id, sizeof(int), 1, file);
        fwrite(&n->right->id, sizeof(int), 1, file);
    }
    free(nodes);
}

int main()
{
    char key[256];
    unsigned long long int value;
    root = (TNode *) malloc(sizeof(TNode));
    PatriciaNodeCreate(root, -1, NULL, 0, root, root);
    int flag = 0;
    while (scanf("%s", key) != EOF) {
        if (!strcmp(key, "+")) {
            scanf("%s", key);
            ToLowerCase(key);
            scanf("%llu", &value);
            if (Insert(key, value)) {
                printf("OK\n");
            } else {
                printf("Exist\n");
            }
        } else if (!strcmp(key, "-")) {
            scanf("%s", key);
            ToLowerCase(key);
            if (Erace(key)) {
                printf("OK\n");
            } else {
                printf("NoSuchWord\n");
            }
        } else if (!strcmp(key, "!")) {
            scanf("%s", key);
            if (!strcmp(key, "Save")) {
                scanf("%s", key);
                FILE *f = fopen(key, "wb");
                if (size == 0){
                    flag = 1;
                }
                else {
                    flag = 0;
                }
                if (f != NULL) {
                    Save(f);
                    fclose(f);
                }
                printf("OK\n");
            } else {
                scanf("%s", key);
                FILE *file = fopen(key, "rb");
                if ((file == NULL) || (flag == 1)) {
                    Destroy(root);
                    root = (TNode *) malloc(sizeof(TNode));
                    PatriciaNodeCreate(root, -1, 0, 0, root, root);
                    printf("OK\n");
                }
                else {
                    Destroy(root);
                    root = (TNode *) malloc(sizeof(TNode));
                    PatriciaNodeCreate(root, -1, 0, 0, root, root);
                    int sz;
                    fread(&sz, sizeof(int), 1, file);
                    size = sz;
                    if (!size) {
                        continue;
                    }
                    TNode **nodes = (TNode **) malloc(sizeof(TNode *) * (size + 1));
                    nodes[0] = root;
                    for (int i = 1; i < size + 1; i++) {
                        nodes[i] = (TNode *) malloc(sizeof(TNode));
                        PatriciaNodeCreate(nodes[i], -1, 0, 0, nodes[i], nodes[i]); 
                    }
                    int bit, len, idl, idr;
                    char *k;
                    unsigned long long int v;
                    for (int i = 0; i < sz + 1; i++) {
                        fread(&v, sizeof(unsigned long long int), 1, file);
                        fread(&bit, sizeof(int), 1, file);
                        fread(&len, sizeof(int), 1, file);
                        if (len > 0) {
                            k = (char *) malloc(len + 1);
                            k[len] = 0;
                        }
                        fread(k, len, 1, file);
                        fread(&idl, sizeof(int), 1, file);
                        fread(&idr, sizeof(int), 1, file);
                        PatriciaNodeCreate(nodes[i], bit, k, v, nodes[idl], nodes[idr]);
                        if (len > 0) {
                            free(k);
                        }
                    }
                    free(nodes);
                    fclose(file);
                    printf("OK\n");
                }
            }
        } else {
            ToLowerCase(key);
            TNode *n = Find(key);
            if (n == NULL) {
                printf("NoSuchWord\n");
            } else {
                printf("OK: %llu\n", n->value);
            }
        }
    }
    Destroy(root);
    root = NULL;
    size = 0;
    return 0;
}