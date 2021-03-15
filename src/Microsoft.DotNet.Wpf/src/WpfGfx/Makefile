
.SUFFIXES: #disable all builtin rules

C_SRCS=$(shell cat sources.list)

DEF_SRC=core/dll/wpfgfx.def

INCLUDE_DIRS= \
	../Shared/inc \
	shared/inc \
	include \
	include/Generated \
	include/processed \
	common \
	common/DirectXLayer \
	common/DirectXLayer/interfaces \
	common/DirectXLayer/factories \
	shared/inc \
	shared/util/utillib \
	core \
	core/sw \
	../Shared/Tracing/native \
	../Shared/internal/multimedia/inc \
	core/fxjit/public \
	core/fxjit/Compiler \
	../../../../wine \
	../../../..

all: $(OBJDIR)/wpfgfx_cor3.dll
.PHONY: all

CXX_FLAGS_arm=-D_ARM_
CXX_FLAGS_arm64=-D_ARM_

CXX_FLAGS=-c -Wall -Wno-format -Wno-unknown-pragmas -Wno-endif-labels -Wno-reorder -Wno-conversion-null -Wno-overloaded-virtual -Wno-inline-new-delete -Wno-ignored-pragma-optimize -Wno-undefined-inline -Wno-inconsistent-missing-override -Wno-deprecated-register -Wno-undefined-var-template -Wno-switch -Wno-missing-braces -Wno-parentheses-equality -Wno-implicit-int-float-conversion -Wno-non-c-typedef-for-linkage -Wno-unused-private-field -Werror -fms-extensions -municode -DD3D9EXTENSIONS -DNOASM -D_UNICODE $(CXX_FLAGS_$(ARCH)) $(INCLUDE_DIRS:%=-I%)

C_FLAGS=-c -D_WIN32_WINNT=0x0602 -D__WINESRC__ --std=c99 -Wall -Wno-format -Werror $(INCLUDE_DIRS:%=-I%)

LINK_FLAGS=-lgdi32 -lntdll -ld3dx9 -lwindowscodecs -lwinmm -luuid -ldwrite -lole32 -loleaut32 -static-libstdc++

$(OBJDIR)/Shared/%.cpp.o: ../Shared/%.cpp
	mkdir -p $(@D)
	$(MINGW)-g++ -o $@ $(CXX_FLAGS) $<

$(OBJDIR)/%.cpp.o: %.cpp
	mkdir -p $(@D)
	$(MINGW)-g++ -o $@ $(CXX_FLAGS) $<

$(OBJDIR)/%.cxx.o: %.cxx
	mkdir -p $(@D)
	$(MINGW)-g++ -o $@ $(CXX_FLAGS) $<

$(OBJDIR)/wine/%.c.o: ../../../../wine/%.c
	mkdir -p $(@D)
	$(MINGW)-gcc -o $@ $(C_FLAGS) $<

$(OBJDIR)/%.rc.res: %.rc
	mkdir -p $(@D)
	$(MINGW)-windres $(INCLUDE_DIRS:%=-I%) -o $@ -i $<

$(OBJDIR)/wpfgfx_cor3.dll: $(C_SRCS:%=$(OBJDIR)/%.o) $(OBJDIR)/core/hw/hw.rc.res $(DEF_SRC)
	$(MINGW)-g++ -o $@ -shared -Wl,--kill-at $(LINK_FLAGS) $(C_SRCS:%=$(OBJDIR)/%.o) $(OBJDIR)/core/hw/hw.rc.res $(DEF_SRC)
