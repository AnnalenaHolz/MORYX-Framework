/*
 * Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
 * Licensed under the Apache License, Version 2.0
*/

import { mdiChevronDown, mdiFolderOpen, mdiPlus, mdiTrashCanOutline } from "@mdi/js";
import Icon from "@mdi/react";
import * as React from "react";
import { Button, ButtonGroup, Col, Collapse, Container, DropdownItem, DropdownMenu, DropdownToggle, Input, Row } from "reactstrap";
import Entry from "../../models/Entry";
import { EntryValueType } from "../../models/EntryValueType";
import BooleanEditor from "./BooleanEditor";
import ByteEditor from "./ByteEditor";
import CollapsibleEntryEditorBase, { CollapsibleEntryEditorBasePropModel } from "./CollapsibleEntryEditorBase";
import ConfigEditor from "./ConfigEditor";
import EnumEditor from "./EnumEditor";
import NumberEditor from "./NumberEditor";
import StringEditor from "./StringEditor";

interface CollectionEditorStateModel {
    SelectedEntry: string;
    ExpandedEntryNames: string[];
}

export default class CollectionEditor extends CollapsibleEntryEditorBase<CollectionEditorStateModel> {
    constructor(props: CollapsibleEntryEditorBasePropModel) {
        super(props);
        this.state = {
            SelectedEntry: props.Entry.value.possible[0],
            ExpandedEntryNames: [],
        };
    }

    public toggleCollapsible(entryName: string): void {
        if (this.isExpanded(entryName)) {
            this.setState((prevState) => ({ ExpandedEntryNames: prevState.ExpandedEntryNames.filter((name) => name !== entryName) }));
        } else {
            this.setState((prevState) => ({ ExpandedEntryNames: [...prevState.ExpandedEntryNames, entryName] }));
        }
    }

    public isExpanded(entryName: string): boolean {
        return this.state.ExpandedEntryNames.find((e: string) => e === entryName) != undefined;
    }

    public onSelect(e: React.FormEvent<HTMLInputElement>): void {
        this.setState({ SelectedEntry: e.currentTarget.value });
    }

    public addEntry(): void {
        const prototype = this.props.Entry.prototypes.find((proto: Entry) => proto.displayName === this.state.SelectedEntry);
        const entryPrototype = Entry.entryFromPrototype(prototype, this.props.Entry);
        entryPrototype.identifier = "CREATED";

        let counter: number = 0;
        let entryName: string = entryPrototype.displayName;

        while (this.props.Entry.subEntries.find((subEntry: Entry) => subEntry.displayName === entryName) !== undefined) {
            ++counter;
            entryName = entryPrototype.displayName + " " + counter.toString();
        }

        entryPrototype.displayName = entryName;
        this.props.Entry.subEntries.push(entryPrototype);

        this.forceUpdate();
    }

    public removeEntry(entry: Entry): void {
        this.props.Entry.subEntries.splice(this.props.Entry.subEntries.indexOf(entry), 1);
        this.forceUpdate();
    }

    public preRenderOptions(): React.ReactNode {
        const options: React.ReactNode[] = [];
        this.props.Entry.value.possible.map((colEntry, idx) =>
        (
            options.push(<option key={idx} value={colEntry}>{colEntry}</option>)
        ));
        return options;
    }

    public preRenderConfigEditor(entry: Entry): React.ReactNode {
        if (!Entry.isClassOrCollection(entry)) {
            switch (entry.value.type) {
                case EntryValueType.Byte: {
                    return (<ByteEditor Entry={entry} IsReadOnly={this.props.IsReadOnly} />);
                }
                case EntryValueType.Boolean: {
                    return (<BooleanEditor Entry={entry} IsReadOnly={this.props.IsReadOnly} />);
                }
                case EntryValueType.Int16:
                case EntryValueType.UInt16:
                case EntryValueType.Int32:
                case EntryValueType.UInt32:
                case EntryValueType.Int64:
                case EntryValueType.UInt64:
                case EntryValueType.Single:
                case EntryValueType.Double: {
                    return (<NumberEditor Entry={entry} IsReadOnly={this.props.IsReadOnly} />);
                }
                case EntryValueType.String: {
                    return (<StringEditor Entry={entry} IsReadOnly={this.props.IsReadOnly} />);
                }
                case EntryValueType.Enum: {
                    return (<EnumEditor Entry={entry} IsReadOnly={this.props.IsReadOnly} />);
                }
            }

            return (<span>Not implemented yet: {entry.value.type}</span>);
        }

        return <ConfigEditor ParentEntry={entry}
                             Entries={entry.subEntries}
                             Root={this.props.Root}
                             navigateToEntry={this.props.navigateToEntry}
                             IsReadOnly={this.props.IsReadOnly} />;
    }

    public render(): React.ReactNode {
        return (
            <div className="up-space">
                <Collapse isOpen={this.props.IsExpanded}>
                    <Container fluid={true} className="no-padding up-space down-space">
                        {
                            this.props.Entry.subEntries.map((entry, idx) => {
                                if (Entry.isClassOrCollection(entry)) {
                                    return (
                                        <div key={idx}>
                                            <Row className="table-row down-space">
                                                <Col md={6} className="no-padding">{entry.displayName}</Col>
                                                <Col md={6} className="no-padding">
                                                    <ButtonGroup>
                                                        <Button color="secondary" onClick={() => this.props.navigateToEntry(entry)}>
                                                            <Icon path={mdiFolderOpen} className="icon right-space" />
                                                            Open
                                                        </Button>
                                                        <Button color="secondary" onClick={() => this.toggleCollapsible(entry.uniqueIdentifier)}>
                                                            <Icon path={mdiChevronDown} className="icon right-space" />
                                                            Expand
                                                        </Button>
                                                        <Button color="secondary" onClick={() => this.removeEntry(entry)} disabled={this.props.Entry.value.isReadOnly || this.props.IsReadOnly}>
                                                            <Icon path={mdiTrashCanOutline} className="icon right-space" />
                                                            Remove
                                                        </Button>
                                                    </ButtonGroup>
                                                </Col>
                                            </Row>
                                            <Collapse isOpen={this.isExpanded(entry.uniqueIdentifier)}>
                                                {this.preRenderConfigEditor(entry)}
                                            </Collapse>
                                        </div>
                                    );
                                } else {
                                    return (
                                        <div key={idx}>
                                            <Row className="table-row down-space">
                                                <Col md={6} className="no-padding">{this.preRenderConfigEditor(entry)}</Col>
                                                <Col md={6} className="no-padding">
                                                    <ButtonGroup>
                                                        <Button color="secondary" onClick={() => this.removeEntry(entry)} disabled={this.props.Entry.value.isReadOnly || this.props.IsReadOnly}>
                                                            <Icon path={mdiTrashCanOutline} className="icon right-space" />
                                                            Remove
                                                        </Button>
                                                    </ButtonGroup>
                                                </Col>
                                            </Row>
                                        </div>
                                    );
                                }
                            })
                        }
                        <Row>
                            <Col md={12} className="no-padding">
                                <Input type="select" value={this.state.SelectedEntry}
                                       className="right-space"
                                       style={{display: "inline", width: "60%"}}
                                       disabled={this.props.Entry.value.isReadOnly || this.props.IsReadOnly}
                                       onChange={(e: React.FormEvent<HTMLInputElement>) => this.onSelect(e)}>
                                    {this.preRenderOptions()}
                                </Input>
                                <Button color="primary" onClick={() => this.addEntry()} disabled={this.props.Entry.value.isReadOnly || this.props.IsReadOnly}>
                                    <Icon path={mdiPlus} className="icon-white right-space" />
                                    Add entry
                                </Button>
                            </Col>
                        </Row>
                    </Container>
                </Collapse>
            </div>
        );
    }
}
