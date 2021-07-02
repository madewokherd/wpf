
C_SRCS=main.c

all: wmwpfdwhelper.dll
.PHONY: all

%.o: $(SRCDIR)/%.c
	$(MINGW)-gcc -o $@ $(CFLAGS) -D_WIN32_WINNT=0x0602 --std=c99 -c -Wall -Wno-format -Werror -I$(SRCDIR)/.. $<

wmwpfdwhelper.dll: $(C_SRCS:%.c=%.o)
	$(MINGW)-gcc -o $@ $(LDFLAGS) -shared -Wl,--kill-at $(C_SRCS:%.c=%.o) -luuid -lgdi32
