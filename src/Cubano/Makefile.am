ASSEMBLY = $(top_builddir)/bin/Cubano.exe

SOURCES =  \
	Banshee.Gui.Widgets/ActionLabel.cs \
	Banshee.Gui.Widgets/BubbleWindow.cs \
	Banshee.Gui.Widgets/ConnectedSeekableTrackInfoDisplay.cs \
	Banshee.Gui.Widgets/CoverArtBrush.cs \
	Banshee.Gui.Widgets/CoverArtDisplay.cs \
	Banshee.Gui.Widgets/SeekableTrackInfoDisplay.cs \
	Banshee.Gui.Widgets/ShapedPopupWindow.cs \
	Banshee.Sources.Gui/CategorySourceView.cs \
	Cubano.Client/CubanoClient.cs \
	Cubano.Client/CubanoSourceContents.cs \
	Cubano.Client/CubanoSourcePopupWindow.cs \
	Cubano.Client/CubanoTaskStatusButton.cs \
	Cubano.Client/CubanoTheme.cs \
	Cubano.Client/CubanoTitleCell.cs \
	Cubano.Client/CubanoTrackTitleView.cs \
	Cubano.Client/CubanoWindow.cs \
	Cubano.Client/CubanoWindowDecorator.cs \
	Cubano.Client/CunanoHeader.cs \
	Cubano.Client/LtrTrackSourceContents.cs \
	Cubano.Client/SearchEntry.cs \
	Cubano.Client/ViewContainer.cs \
	Cubano.Client/WindowDecorator.cs \
	Cubano.NowPlaying/ArtworkDisplay.cs \
	Cubano.NowPlaying/CubanoClutterVisualizer.cs \
	Cubano.NowPlaying/CubanoVisualizer.cs \
	Cubano.NowPlaying/NowPlayingInterface.cs \
	Cubano.NowPlaying/NowPlayingSource.cs \
	Cubano.NowPlaying/NowPlayingStage.cs

RESOURCES =

REFERENCES_PACKAGES =  \
	-pkg:banshee-1-core \
	-pkg:banshee-1-hyena \
	-pkg:banshee-1-hyena-gui \
	-pkg:banshee-1-services \
	-pkg:banshee-1-thickclient \
	-pkg:clutter-gtk-sharp \
	-pkg:clutter-sharp \
	-pkg:glib-sharp-3.0 \
	-pkg:gtk-sharp-3.0 \
	-pkg:taglib-sharp

RESOURCES_BUILD = $(foreach res, $(RESOURCES), \
	-resource:$(notdir $(res)),$(res))

REFERENCES_ASSEMBLIES =
REFERENCES_ASSEMBLIES_BUILD = $(foreach ref, $(REFERENCES_ASSEMBLIES), \
	-r:$(srcdir)/$(ref))

REFERENCES_BUILD = $(BANSHEE_LIBS) $(CLUTTER_LIBS) $(COGL_LIBS) $(REFERENCES_ASSEMBLIES_BUILD)

all: $(ASSEMBLY)

$(ASSEMBLY): $(SOURCES) $(RESOURCES)
	$(MCS) -out:$@ -debug $(REFERENCES_BUILD) $(RESOURCES_BUILD) $(addprefix $(srcdir)/, $(SOURCES))

run:
	@(cd $(top_builddir)/bin; make run)

EXTRA_DIST = $(SOURCES)
CLEANFILES = $(ASSEMBLY){,.mdb}
MAINTAINERCLEANFILES = Makefile.in
