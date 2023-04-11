
COMPILE_LIB=csc -target:library

LIB_FLAGS=-unsafe -delaysign+ -keyfile:../../reactive.pub -define:CORE_NATIVEMETHODS -define:PRESENTATION_CORE -define:COMMONDPS -langversion:8.0 -nostdlib -noconfig -lib:$(MONO_PREFIX)/lib/mono/4.5-api

LIB_REFS=-r:mscorlib.dll -r:System.dll -r:System.Xml.dll -r:System.Core.dll -r:System.Security.dll -r:System.Configuration.dll -r:System.IO.Compression.dll

LIB_REF_FILES=../System.Xaml/System.Xaml.dll ../WindowsBase/WindowsBase.dll ../System.Windows.Input.Manipulations/System.Windows.Input.Manipulations.dll ../UIAutomation/UIAutomationTypes/UIAutomationTypes.dll ../UIAutomation/UIAutomationProvider/UIAutomationProvider.dll

LIB_SRCS=$(shell cat sources.list)

all: PresentationCore.dll

SRID.generated.cs: Resources/Strings.resx $(RESX2SRID)
	mono $(RESX2SRID) $< SRID MS.Internal.PresentationCore > $@ || rm -f $@

%.resources: %.resx
	resgen -useSourcePath $< $@

Resources/ColorProfiles.resources: System/Windows/Media/Resources/ColorProfiles/ColorProfiles.resx
	resgen -useSourcePath $< $@

Resources/PresentationCore.g.resources: Fonts/*.CompositeFont

EMBEDDED_RESOURCES=$(shell cat embeddedresources.list)

EMBEDDED_RES_PATHS=$(foreach f,$(EMBEDDED_RESOURCES),Resources/$(f))

embeddedresources.flags: embeddedresources.list
	for i in `cat embeddedresources.list`; do printf -- '-resource:Resources/%s,%s\n' $$i `echo $$i|sed "s:/:.:g"|sed "s:Strings.resources:SR.resources:g"`; done > $@ || rm -f $@

PresentationCore.dll: sources.list $(LIB_SRCS) $(LIB_REF_FILES) embeddedresources.flags $(EMBEDDED_RES_PATHS)
	$(COMPILE_LIB) $(LIB_FLAGS) $(LIB_REFS) $(LIB_REF_FILES:%=-r:%) -out:$@ @sources.list @embeddedresources.flags
	sn -R $@ ../../mono.snk
