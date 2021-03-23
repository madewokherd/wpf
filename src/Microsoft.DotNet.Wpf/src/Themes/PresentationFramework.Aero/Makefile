
COMPILE_LIB=csc -target:library

LIB_FLAGS=-unsafe -delaysign+ -keyfile:../../../reactive.pub -define:THEME_AERO -langversion:8.0 -nostdlib -noconfig

LIB_REFS=-r:mscorlib.dll -r:System.dll -r:System.Xaml.dll -r:System.Xml.dll -r:System.Core.dll

LIB_REF_FILES=../../System.Xaml/System.Xaml.dll ../../PresentationUI/PresentationUI.dll ../../PresentationFramework/PresentationFramework.dll ../../WindowsBase/WindowsBase.dll ../../PresentationCore/PresentationCore.dll

LIB_SRCS=$(shell cat sources.list)

all: PresentationFramework.Aero.dll

%.resources: %.resx
	resgen -useSourcePath $< $@

Resources/PresentationFramework.aero.g.resources: themes/Aero.NormalColor.xaml

EMBEDDED_RESOURCES=$(shell cat embeddedresources.list)

EMBEDDED_RES_PATHS=$(foreach f,$(EMBEDDED_RESOURCES),Resources/$(f))

embeddedresources.flags: embeddedresources.list
	for i in `cat embeddedresources.list`; do printf -- '-resource:Resources/%s,%s\n' $$i `echo $$i|sed "s:/:.:g"`; done > $@ || rm -f $@

PresentationFramework.Aero.dll: sources.list $(LIB_SRCS) $(LIB_REF_FILES) embeddedresources.flags $(EMBEDDED_RES_PATHS) $(ACCESSIBILITY_DLL)
	$(COMPILE_LIB) $(LIB_FLAGS) $(LIB_REFS) $(LIB_REF_FILES:%=-r:%) -out:$@ @sources.list @embeddedresources.flags
	sn -R $@ ../../../mono.snk
