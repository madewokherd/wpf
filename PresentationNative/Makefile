C_SRCS=wrappers.c classification.c

all: PresentationNative_cor3.dll
.PHONY: all

%.o: $(SRCDIR)/%.c
	$(MINGW)-gcc -o $@ $(CFLAGS) -D_WIN32_WINNT=0x0602 --std=c99 -c -Wall -Werror -Wno-pragma-pack $<

classification.o: $(SRCDIR)/class_table.h

PresentationNative_cor3.dll: $(C_SRCS:%.c=%.o)
	$(MINGW)-gcc -o $@ $(LDFLAGS) -shared -Wl,--kill-at $(C_SRCS:%.c=%.o) -luuid -lgdi32
