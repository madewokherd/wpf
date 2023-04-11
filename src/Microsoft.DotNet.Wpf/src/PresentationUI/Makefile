
COMPILE_LIB=csc -target:library

LIB_FLAGS=-unsafe -delaysign+ -keyfile:../../reactive.pub -define:PRESENTATIONUI -langversion:8.0 -nostdlib -noconfig

LIB_REFS=-r:mscorlib.dll -r:System.dll -r:System.Xml.dll -r:System.Core.dll -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:System.DirectoryServices.dll -r:System.Security.dll

LIB_REF_FILES=../System.Xaml/System.Xaml.dll ../PresentationFramework/PresentationFramework.dll ../WindowsBase/WindowsBase.dll ../UIAutomation/UIAutomationTypes/UIAutomationTypes.dll ../UIAutomation/UIAutomationProvider/UIAutomationProvider.dll ../PresentationCore/PresentationCore.dll ../ReachFramework/ReachFramework.dll ../System.Printing/ref/System.Printing.dll

LIB_SRCS=$(shell cat sources.list)

all: PresentationUI.dll

../System.Printing/ref/System.Printing.dll: ../System.Printing/ref/*.cs
	+$(MAKE) -C $(@D)
	touch $@

SRID.generated.cs: Resources/Strings.resx $(RESX2SRID)
	mono $(RESX2SRID) $< SRID System.Windows.TrustUI > $@ || rm -f $@

XAML2CS=../../../../xaml2cs/xaml2cs.exe

$(XAML2CS): $(XAML2CS:.exe=.cs)
	+$(MAKE) -C $(@D)

%.xaml.generated.cs: %.xaml $(XAML2CS)
	(mono $(XAML2CS) $< > $@) || (rm $@; false)

%.resources: %.resx
	resgen -useSourcePath $< $@

EMBEDDED_RESOURCES=$(shell cat embeddedresources.list)

EMBEDDED_RES_PATHS=$(foreach f,$(EMBEDDED_RESOURCES),Resources/$(f))

embeddedresources.flags: embeddedresources.list
	for i in `cat embeddedresources.list`; do printf -- '-resource:Resources/%s,%s\n' $$i `echo $$i|sed "s:/:.:g"|sed "s:Strings.resources:SR.resources:g"`; done > $@ || rm -f $@

PresentationUI.dll: sources.list $(LIB_SRCS) $(LIB_REF_FILES) embeddedresources.flags $(EMBEDDED_RES_PATHS)
	$(COMPILE_LIB) $(LIB_FLAGS) $(LIB_REFS) $(LIB_REF_FILES:%=-r:%) -out:$@ @sources.list @embeddedresources.flags
	sn -R $@ ../../mono.snk
