
COMPILE_LIB=csc -target:library

LIB_FLAGS=-unsafe -delaysign+ -keyfile:../../reactive.pub -define:FRAMEWORK_NATIVEMETHODS -define:COMMONDPS -define:PRESENTATIONFRAMEWORK_ONLY -define:PRESENTATIONFRAMEWORK -define:RIBBON_IN_FRAMEWORK -langversion:8.0 -nostdlib -noconfig -lib:$(MONO_PREFIX)/lib/mono/4.5-api

LIB_REFS=-r:mscorlib.dll -r:System.dll -r:System.Xml.dll -r:System.Core.dll -r:System.Security.dll -r:System.Configuration.dll -r:System.IO.Compression.dll

LIB_REF_FILES=../System.Xaml/System.Xaml.dll ../WindowsBase/WindowsBase.dll ../UIAutomation/UIAutomationTypes/UIAutomationTypes.dll ../UIAutomation/UIAutomationProvider/UIAutomationProvider.dll ../PresentationCore/PresentationCore.dll ../ReachFramework/ReachFramework.dll ../../cycle-breakers/PresentationUI/PresentationUI-PresentationFramework-impl-cycle.dll ../System.Printing/ref/System.Printing.dll $(ACCESSIBILITY_DLL)

LIB_SRCS=$(shell cat sources.list)

all: PresentationFramework.dll

../../cycle-breakers/PresentationUI/PresentationUI-PresentationFramework-impl-cycle.dll: ../../cycle-breakers/PresentationUI/*.cs
	+$(MAKE) -C $(@D) -f PresentationUI-PresentationFramework-impl-cycle.make
	touch $@

../System.Printing/ref/System.Printing.dll: ../System.Printing/ref/*.cs
	+$(MAKE) -C $(@D)
	touch $@

SRID.generated.cs: Resources/Strings.resx $(RESX2SRID)
	mono $(RESX2SRID) $< SRID System.Windows > $@ || rm -f $@

%.resources: %.resx
	resgen $< $@

PERL=perl

ColumnDefinition.cs: MS/Utility/GridContentElementCollection.tb MS/Utility/GridContentElementCollection.th MS/Utility/ColumnDefinition.ti
	$(PERL) template.pl MS/Utility/GridContentElementCollection MS/Utility/ColumnDefinition > $@ || rm -f $@

RowDefinition.cs: MS/Utility/GridContentElementCollection.tb MS/Utility/GridContentElementCollection.th MS/Utility/RowDefinition.ti
	$(PERL) template.pl MS/Utility/GridContentElementCollection MS/Utility/RowDefinition > $@ || rm -f $@

EMBEDDED_RESOURCES=$(shell cat embeddedresources.list)

EMBEDDED_RES_PATHS=$(foreach f,$(EMBEDDED_RESOURCES),Resources/$(f))

embeddedresources.flags: embeddedresources.list
	for i in `cat embeddedresources.list`; do printf -- '-resource:Resources/%s,%s\n' $$i `echo $$i|sed "s:/:.:g"|sed "s:Strings.resources:SR.resources:g"`; done > $@ || rm -f $@

PresentationFramework.dll: sources.list $(LIB_SRCS) $(LIB_REF_FILES) embeddedresources.flags $(EMBEDDED_RES_PATHS)
	$(COMPILE_LIB) $(LIB_FLAGS) $(LIB_REFS) $(LIB_REF_FILES:%=-r:%) -out:$@ @sources.list @embeddedresources.flags
	sn -R $@ ../../mono.snk
