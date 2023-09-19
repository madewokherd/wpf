
COMPILE_LIB=csc -target:library

LIB_FLAGS=-unsafe -delaysign+ -keyfile:../../ecma.pub -define:RIBBON_IN_FRAMEWORK -langversion:9.0 -nostdlib -noconfig

LIB_REFS=-r:mscorlib.dll -r:System.dll -r:System.Xml.dll

LIB_REF_FILES=../System.Xaml/System.Xaml.dll ../WindowsBase/WindowsBase.dll ../PresentationCore/PresentationCore.dll ../PresentationFramework/PresentationFramework.dll ../UIAutomation/UIAutomationProvider/UIAutomationProvider.dll ../UIAutomation/UIAutomationTypes/UIAutomationTypes.dll ../Themes/PresentationFramework.Classic/PresentationFramework.Classic.dll

LIB_SRCS=$(shell cat sources.list)

all: System.Windows.Controls.Ribbon.dll

SRID.generated.cs: Resources/Strings.resx $(RESX2SRID)
	mono $(RESX2SRID) $< SRID Microsoft.Windows.Controls > $@ || rm -f $@

%.resources: %.resx
	resgen $< $@

EMBEDDED_RESOURCES=$(shell cat embeddedresources.list)

EMBEDDED_RES_PATHS=$(foreach f,$(EMBEDDED_RESOURCES),Resources/$(f))

embeddedresources.flags: embeddedresources.list
	for i in `cat embeddedresources.list`; do printf -- '-resource:Resources/%s,%s\n' $$i `echo $$i|sed "s:/:.:g"|sed "s:Strings.resources:SR.resources:g"`; done > $@ || rm -f $@

System.Windows.Controls.Ribbon.dll: sources.list $(LIB_SRCS) $(LIB_REF_FILES) embeddedresources.flags $(EMBEDDED_RES_PATHS)
	$(COMPILE_LIB) $(LIB_FLAGS) $(LIB_REFS) $(LIB_REF_FILES:%=-r:%) -out:$@ @sources.list @embeddedresources.flags
	sn -R $@ ../../mono.snk
