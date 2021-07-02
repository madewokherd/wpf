
C_SRCS=stubs.c main.c composition.c channel.c resource.c factory.c hwndtarget.c brush.c

H_SRCS=wpfgfx_private.h ../wine/debug.h ../wine/heap.h

all: wpfgfx_cor3.dll
.PHONY: all

%.o: $(SRCDIR)/%.c
	$(MINGW)-gcc -o $@ $(CFLAGS) -D_WIN32_WINNT=0x0602 --std=c99 -c -Wall -Wno-format -Werror -Wno-pragma-pack -I$(SRCDIR)/.. $<

debug.o: $(SRCDIR)/../wine/debug.c
	$(MINGW)-gcc -o $@ $(CFLAGS) -D_WIN32_WINNT=0x0602 -D__WINESRC__ --std=c99 -c -Wall -Wno-format -Werror -I$(SRCDIR)/.. $<

wpfgfx_cor3.dll: $(C_SRCS:%.c=%.o) debug.o $(H_SRCS:%=$(SRCDIR)/%)
	$(MINGW)-gcc -o $@ $(LDFLAGS) -shared -Wl,--kill-at $(C_SRCS:%.c=%.o) debug.o -luuid -lgdi32
